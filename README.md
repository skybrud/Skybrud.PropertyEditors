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

### Notify Page
DataEdtior to handle mails send out to subscribers of a certain page. For this to work, you need to collect your subscribers in a custom-table "umbracoNodeSubscribers" 

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
