﻿using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.PropertyEditors.LinkPicker {

    /// <summary>
    /// Class representing the model for the LinkPicker editor.
    /// </summary>
    public class LinkPickerModel {

        #region Properties

        /// <summary>
        /// Gets an array of all link items.
        /// </summary>
        public LinkPickerItem[] Items { get; internal set; }

        /// <summary>
        /// Gets the total amount of link items.
        /// </summary>
        public int Count {
            get { return Items.Length; }
        }

        #endregion

        #region Constructors

        internal LinkPickerModel() { }

        #endregion

        #region Static methods

        /// <summary>
        /// Deseralizes the specified JSON string into an instance of <code>LinkPickerModel</code>.
        /// </summary>
        /// <param name="json">The raw JSON to be parsed.</param>
        public static LinkPickerModel Deserialize(string json) {

            if (json != null && json.StartsWith("[") && json.EndsWith("]")) {
                JArray array = JsonConvert.DeserializeObject<JArray>(json);
                return new LinkPickerModel {
                    Items = (
                        from obj in array
                        let link = LinkPickerItem.Parse(obj as JObject)
                        where link != null
                        select link
                    ).ToArray()
                };
            }

            return new LinkPickerModel {
                Items = new LinkPickerItem[0]
            };

        }

        #endregion

    }

}