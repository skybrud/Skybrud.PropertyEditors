# Skybrud.PropertyEditors
Various common purpose property editors for Umbraco 7.


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
