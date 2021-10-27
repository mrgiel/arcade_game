CREATE TABLE [dbo].[Game] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [Teamnaam]  NCHAR (16) NOT NULL,
    [Speler1]   NCHAR (16) NOT NULL,
    [Speler2]   NCHAR (16) NOT NULL,
    [Highscore] INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
