using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Skybrud.Umbraco.Module.ErrorHandling;

namespace Skybrud.PropertyEditors.NotifyPage {
    
  
    [PluginController("NotifyPage")]
    [JsonOnlyConfiguration]
    public class NotifyPageApiController : UmbracoAuthorizedApiController
    {

        public object GetSubscribersByPageId(int id)
        {
            try
            {
                return Request.CreateResponse(JsonMetaResponse.GetSuccess(GetSubscribers(id)));
            }
            catch (Exception ex)
            {
                var error = new Error("Server Error");
                LogHelper.Error<NotifyPageApiController>(error.ToString(), ex);
                return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.InternalServerError, error.Message, error));
            }
        }


        [HttpGet]
        public object SendMessages(int id)
        {

            try
            {
                var _helper = new UmbracoHelper(UmbracoContext.Current);

                var page = _helper.TypedContent(id);

                var subject = "subject";
                var subscribers = GetSubscribers(id);

                foreach (var subscriber in subscribers)
                {
                    // MailContent
                    var content = string.Format(@"Kære abbonnent, <br /><br />Følgende side er blevet opdateret: <a href='{0}'>{0}</a><br />", page.UrlWithDomain());

                    SendMail(subject, content, subscriber);
                }

                return Request.CreateResponse(JsonMetaResponse.GetSuccess(true));
            }
            catch(Exception ex)
            {
                var error = new Error("Server Error");
                LogHelper.Error<NotifyPageApiController>(error.ToString(), ex);
                return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.InternalServerError, error.Message, error));
            }
        }

        /// <summary>
        /// Get subscribers to a current page
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private List<string> GetSubscribers(int nodeId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataReader data;
                var output = new List<string>();
                using (var command = connection.CreateCommand())
                {
                    {
                        command.CommandText = "SELECT * FROM umbracoNodeSubscribers WHERE NodeId = @NodeId";
                        command.Parameters.AddWithValue("@NodeId", nodeId);

                        data = command.ExecuteReader();

                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                output.Add(data.GetString(data.GetOrdinal("Email")));
                            }
                        }
                    }
                }

                return output;
            }
        }

        /// <summary>
        /// Send mails to subscribers
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="email"></param>
        private void SendMail(string subject, string body, string email)
        {

            MailMessage msg = new MailMessage
            {
                Subject = subject,
                From = new MailAddress("no-reply@your-website.com", "Your Mailbot"),
                IsBodyHtml = true,
                Body = (body ?? "").Trim()
            };

            msg.To.Add(new MailAddress(email));

            SmtpClient client = new SmtpClient();
            client.Send(msg);

        }
    }
}
