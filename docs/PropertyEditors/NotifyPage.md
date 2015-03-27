# Notify Page Property Editor


### Installation

Add this table to your database.
```SQL
CREATE TABLE [dbo].[umbracoNodeSubscribers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NodeId] [int] NOT NULL,
	[Email] [varchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
```

After that make som API to handle insert and deletion of subscribers. See example:
```CSHARP

[JsonOnlyConfiguration]
    public class SubscribeUpdateApiController : UmbracoApiController 
    {
    	[HttpPost]
        public object SetSubscribeToPage([FromBody] SubscriberParameters data)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    bool alreadySubscribedToNode;
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "SELECT COUNT(*) FROM umbracoNodeSubscribers WHERE NodeId = @NodeId AND Email LIKE @Email";
                        command.Parameters.AddWithValue("@NodeId", data.NodeId);
                        command.Parameters.AddWithValue("@Email", IntranetContext.Current.Member.UmbracoMember.Email);

                        alreadySubscribedToNode = (int)command.ExecuteScalar() > 0;
                    }

                    if (alreadySubscribedToNode == false)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText =
                                "INSERT INTO umbracoNodeSubscribers(NodeId, Email) VALUES(@NodeId, @Email)";
                            command.Parameters.AddWithValue("@NodeId", data.NodeId);
                            command.Parameters.AddWithValue("@Email", IntranetContext.Current.Member.UmbracoMember.Email);

                            command.ExecuteNonQuery();
                        }
                    }

                    return Request.CreateResponse(JsonMetaResponse.GetSuccess(true));
                }
            }
            catch(Exception ex)
            {
                var error = new Error("Server Error");
                LogHelper.Error<SubscribeUpdateApiController>(error.ToString(), ex);
                return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.InternalServerError, error.Message, error));
            }
        }

        [HttpGet]
        public object CancelSubscribeToPage(int nodeId)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "DELETE FROM umbracoNodeSubscribers WHERE NodeId = @NodeId AND Email LIKE @Email";
                        command.Parameters.AddWithValue("@NodeId", nodeId);
                        command.Parameters.AddWithValue("@Email", IntranetContext.Current.Member.UmbracoMember.Email);

                        command.ExecuteNonQuery();
                    }
                }

                return Request.CreateResponse(JsonMetaResponse.GetSuccess(true));

            }
            catch (Exception ex)
            {
                var error = new Error("Server Error");
                LogHelper.Error<SubscribeUpdateApiController>(error.ToString(), ex);
                return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.InternalServerError, error.Message, error));
            }
        }

        public class SubscriberParameters
        {
            public int NodeId { get; set; }
            public string Email { get; set; }
        }

	}

```
