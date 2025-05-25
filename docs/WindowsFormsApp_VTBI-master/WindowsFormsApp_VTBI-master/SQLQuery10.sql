-- Creating primary key on [codemethodesortie] in table 'MethodeSortieSet'
ALTER TABLE [dbo].[MethodeSortieSet]
ADD CONSTRAINT [PK_MethodeSortieSet]
    PRIMARY KEY CLUSTERED ([codemethodesortie] ASC);
GO