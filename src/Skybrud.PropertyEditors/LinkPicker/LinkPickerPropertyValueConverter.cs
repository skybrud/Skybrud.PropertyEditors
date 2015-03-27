﻿using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.PropertyEditors.LinkPicker {

    public class LinkPickerPropertyValueConverter : IPropertyValueConverter {

        public bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.PropertyEditorAlias == "Skybrud.PropertyEditors.LinkPicker";
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object data, bool preview) {
            return LinkPickerModel.Deserialize(data as string);
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview) {
            return source;
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview) {
            return null;
        }

    }

}