# Skybrud.PropertyEditors

**Skybrud.PropertyEditors** is a set of various common purpose property editors for Umbraco 7 that we use internally at Skybrud.dk.

- [Download](#download)
- [Property editors](#property-editors)
- [License](https://github.com/skybrud/Skybrud.PropertyEditors/blob/master/LICENSE.md)

## Download

This package is still under development, but will be available for download as both a NuGet package and an Umbraco package. Stay tuned ;)

## Property Editors

- [Link Picker](#link-picker)
- [Notify Page](#notify-page)

### Link Picker

Property editor for managing a collection of links. Depending on it's prevalues, this editor can work both as a multi link picker or a single link picker. A prevalue also lets you set wether the links should be displayed in a simple list or in a table with more details.

**Notice:** The link picker has now been moved to it's own repository/package. It is recommended to use new new link picker package instead: https://github.com/skybrud/Skybrud.LinkPicker

### Notify Page
DataEdtior to handle mails send out to subscribers of a certain page. 
Yet to be done: isolate mailmessage in either .config-file or Umbraco Backoffice

* [See documentation for Notify Page](https://github.com/skybrud/Skybrud.PropertyEditors/blob/master/docs/PropertyEditors/NotifyPage.md)
