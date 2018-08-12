/*
Script de implantação para ClinicaDB

Este código foi gerado por uma ferramenta.
As alterações feitas nesse arquivo poderão causar comportamento incorreto e serão perdidas se
o código for gerado novamente.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ClinicaDB"
:setvar DefaultFilePrefix "ClinicaDB"
:setvar DefaultDataPath "E:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "E:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detecta o modo SQLCMD e desabilita a execução do script se o modo SQLCMD não tiver suporte.
Para reabilitar o script após habilitar o modo SQLCMD, execute o comando a seguir:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'O modo SQLCMD deve ser habilitado para executar esse script com êxito.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Criando $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'As configurações de banco de dados não podem ser modificadas. Você deve ser um SysAdmin para aplicar essas configurações.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'As configurações de banco de dados não podem ser modificadas. Você deve ser um SysAdmin para aplicar essas configurações.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Criando [dbo].[ArquivoMorto]...';


GO
CREATE TABLE [dbo].[ArquivoMorto] (
    [Id]       INT          NOT NULL,
    [Nome]     VARCHAR (50) NOT NULL,
    [Cpf]      VARCHAR (11) NOT NULL,
    [Email]    VARCHAR (50) NOT NULL,
    [DataNasc] DATE         NOT NULL,
    [Genero]   CHAR (1)     NOT NULL,
    [Celular]  VARCHAR (11) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[ArquivoMortoHistoricos]...';


GO
CREATE TABLE [dbo].[ArquivoMortoHistoricos] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Historioco] VARCHAR (300) NOT NULL,
    [IdPaciente] INT           NOT NULL
);


GO
PRINT N'Criando [dbo].[Atendimento]...';


GO
CREATE TABLE [dbo].[Atendimento] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [IdClinica] INT NOT NULL,
    [IdMedico]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[Clinica]...';


GO
CREATE TABLE [dbo].[Clinica] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Email]           VARCHAR (50) NOT NULL,
    [Cnpj]            VARCHAR (14) NOT NULL,
    [StatusAtividade] BIT          NOT NULL,
    [TelefoneCom]     VARCHAR (10) NOT NULL,
    [Nome]            VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Cnpj] ASC)
);


GO
PRINT N'Criando [dbo].[Consulta]...';


GO
CREATE TABLE [dbo].[Consulta] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Historico]     VARCHAR (300) NOT NULL,
    [Nota]          TINYINT       NOT NULL,
    [DataHora]      DATETIME      NOT NULL,
    [IdPaciente]    INT           NOT NULL,
    [IdAtendimento] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[Endereco]...';


GO
CREATE TABLE [dbo].[Endereco] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Estado]      CHAR (2)     NOT NULL,
    [Cidade]      VARCHAR (50) NOT NULL,
    [Bairro]      VARCHAR (50) NOT NULL,
    [Logradouro]  VARCHAR (50) NOT NULL,
    [Numero]      INT          NOT NULL,
    [Complemento] VARCHAR (50) NULL,
    [IdClinica]   INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[Especialidade]...';


GO
CREATE TABLE [dbo].[Especialidade] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Nome] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Nome] ASC)
);


GO
PRINT N'Criando [dbo].[Exame]...';


GO
CREATE TABLE [dbo].[Exame] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [DataHora]      DATETIME NOT NULL,
    [IdAtendimento] INT      NOT NULL,
    [IdConsulta]    INT      NOT NULL,
    [IdTipoExame]   INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[HorariosConsulta]...';


GO
CREATE TABLE [dbo].[HorariosConsulta] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [Dia]           DATE     NOT NULL,
    [Hora]          TIME (7) NOT NULL,
    [Valor]         MONEY    NOT NULL,
    [IdAtendimento] INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[HorariosExame]...';


GO
CREATE TABLE [dbo].[HorariosExame] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [IdClinica] INT      NOT NULL,
    [Data]      DATE     NOT NULL,
    [Hora]      TIME (7) NOT NULL,
    [Valor]     MONEY    NOT NULL
);


GO
PRINT N'Criando [dbo].[Medico]...';


GO
CREATE TABLE [dbo].[Medico] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Nome]            VARCHAR (50) NOT NULL,
    [Cpf]             VARCHAR (11) NOT NULL,
    [Crm]             VARCHAR (10) NOT NULL,
    [Email]           VARCHAR (50) NOT NULL,
    [Senha]           VARCHAR (20) NOT NULL,
    [DataNasc]        DATE         NOT NULL,
    [StatusAtividade] BIT          NOT NULL,
    [Genero]          CHAR (1)     NOT NULL,
    [Celular]         VARCHAR (11) NOT NULL,
    [IdEspecialidade] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[Paciente]...';


GO
CREATE TABLE [dbo].[Paciente] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (50) NOT NULL,
    [Cpf]         VARCHAR (11) NOT NULL,
    [Email]       VARCHAR (50) NOT NULL,
    [Senha]       VARCHAR (20) NOT NULL,
    [DataNasc]    DATE         NOT NULL,
    [Genero]      CHAR (1)     NOT NULL,
    [Celular]     VARCHAR (11) NOT NULL,
    [TelefoneRes] VARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Cpf] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Criando [dbo].[TipoExame]...';


GO
CREATE TABLE [dbo].[TipoExame] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Nome] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Criando [dbo].[FK_ArquivoMortoHistorico]...';


GO
ALTER TABLE [dbo].[ArquivoMortoHistoricos]
    ADD CONSTRAINT [FK_ArquivoMortoHistorico] FOREIGN KEY ([IdPaciente]) REFERENCES [dbo].[ArquivoMorto] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Atendimento_Clinica]...';


GO
ALTER TABLE [dbo].[Atendimento]
    ADD CONSTRAINT [FK_Atendimento_Clinica] FOREIGN KEY ([IdClinica]) REFERENCES [dbo].[Clinica] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Atendimento_Medico]...';


GO
ALTER TABLE [dbo].[Atendimento]
    ADD CONSTRAINT [FK_Atendimento_Medico] FOREIGN KEY ([IdMedico]) REFERENCES [dbo].[Medico] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Consulta_Paciente]...';


GO
ALTER TABLE [dbo].[Consulta]
    ADD CONSTRAINT [FK_Consulta_Paciente] FOREIGN KEY ([IdPaciente]) REFERENCES [dbo].[Paciente] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Consulta_Atendimento]...';


GO
ALTER TABLE [dbo].[Consulta]
    ADD CONSTRAINT [FK_Consulta_Atendimento] FOREIGN KEY ([IdAtendimento]) REFERENCES [dbo].[Atendimento] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Endereco_Clinica]...';


GO
ALTER TABLE [dbo].[Endereco]
    ADD CONSTRAINT [FK_Endereco_Clinica] FOREIGN KEY ([IdClinica]) REFERENCES [dbo].[Clinica] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Exame_TipoExame]...';


GO
ALTER TABLE [dbo].[Exame]
    ADD CONSTRAINT [FK_Exame_TipoExame] FOREIGN KEY ([IdTipoExame]) REFERENCES [dbo].[TipoExame] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Exame_Atendimento]...';


GO
ALTER TABLE [dbo].[Exame]
    ADD CONSTRAINT [FK_Exame_Atendimento] FOREIGN KEY ([IdAtendimento]) REFERENCES [dbo].[Atendimento] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Exame_Consulta]...';


GO
ALTER TABLE [dbo].[Exame]
    ADD CONSTRAINT [FK_Exame_Consulta] FOREIGN KEY ([IdConsulta]) REFERENCES [dbo].[Consulta] ([Id]);


GO
PRINT N'Criando [dbo].[FK_HorariosConsulta_Atendimento]...';


GO
ALTER TABLE [dbo].[HorariosConsulta]
    ADD CONSTRAINT [FK_HorariosConsulta_Atendimento] FOREIGN KEY ([IdAtendimento]) REFERENCES [dbo].[Atendimento] ([Id]);


GO
PRINT N'Criando [dbo].[FK_HorariosExame]...';


GO
ALTER TABLE [dbo].[HorariosExame]
    ADD CONSTRAINT [FK_HorariosExame] FOREIGN KEY ([IdClinica]) REFERENCES [dbo].[Clinica] ([Id]);


GO
PRINT N'Criando [dbo].[FK_Medico_Especialidade]...';


GO
ALTER TABLE [dbo].[Medico]
    ADD CONSTRAINT [FK_Medico_Especialidade] FOREIGN KEY ([IdEspecialidade]) REFERENCES [dbo].[Especialidade] ([Id]);


GO
PRINT N'Criando restrição sem nome em [dbo].[ArquivoMorto]...';


GO
ALTER TABLE [dbo].[ArquivoMorto]
    ADD CHECK ([GENERO] IN ('M', 'F'));


GO
PRINT N'Criando [dbo].[ViewAtendimentos]...';


GO
CREATE VIEW ViewAtendimentos as
select  Atendimento.Id,Medico.Nome as medico, Clinica.Nome as Clinica
From Atendimento
inner Join Medico on Atendimento.IdMedico=Medico.Id
inner join Clinica on Atendimento.IdClinica=Clinica.Id
GO
PRINT N'Criando [dbo].[ViewClinicas]...';


GO
create view ViewClinicas as
select Clinica.Id, Clinica.Email, Clinica.Cnpj, Clinica.StatusAtividade,
Clinica.Nome, endereco.Estado, Endereco.Cidade, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
from Clinica
inner join endereco on endereco.IdClinica=Clinica.Id
GO
PRINT N'Criando [dbo].[ViewConsulta]...';


GO
/* Cria uma view para exibição das consultas,
permitindo selecionar elemetos pelo Id da Consulta, 
Id do Paciente e Id do Médico */

CREATE VIEW ViewConsulta AS
SELECT C.Id, P.Id AS IdPaciente, P.Nome AS Paciente,
M.Id AS IdMedico, M.Nome AS Medico,C.DataHora, C.Historico,
Cl.Id AS IdClinica, Cl.Nome AS Clinica, C.Nota
FROM [Consulta] C
JOIN [Atendimento] A ON C.IdAtendimento = A.Id
JOIN [Medico] M ON A.IdMedico = M.Id
JOIN [Paciente] P ON C.IdPaciente = P.Id
JOIN [Clinica] Cl ON A.IdClinica = Cl.Id
GO
PRINT N'Criando [dbo].[ViewEnderecos]...';


GO
create view ViewEnderecos as
select Endereco.Id as IdEndereco, Clinica.Nome, Endereco.estado,
Endereco.Cidade, Endereco.Bairro, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
FROM Endereco
INNER JOIN Clinica on Endereco.IdClinica = Clinica.Id
GO
PRINT N'Criando [dbo].[ViewExame]...';


GO
/* Cria uma view para exibição dos Exames,
permitindo selecionar elemetos pelo Id do Exame, 
Id do Paciente, Id do Médico que solicitou o exame,
Id do médico que realizou o exame e Id da Clinica onde
o exame foi realizado*/

CREATE VIEW ViewExame AS
SELECT E.Id, TE.Nome AS TipoDoExame, P.Id AS IdPaciente,
P.Nome AS NomeDoPaciente, MedQueSolicitou.Id AS IdMedicoQueSolicitou,
MedQueSolicitou.Nome AS SolicitadoPeloMedico,
Co.DataHora AS DataHora_ExameSolicitado,
E.DataHora AS DataHora_ExameRealizado,
MedQueRealizou.Id AS IdMedicoQueRealizou, MedQueRealizou.Nome AS RealizadoPeloMedico,
Cl.Id AS IdClinica, Cl.Nome AS RealizadoNaClinica
FROM [Exame] E
JOIN [Atendimento] AtRealizado ON E.IdAtendimento = AtRealizado.Id
JOIN [Medico] MedQueRealizou ON AtRealizado.IdMedico = MedQueRealizou.Id
JOIN [TipoExame] TE ON E.IdTipoExame = TE.Id
JOIN [Clinica] Cl ON AtRealizado.IdClinica = Cl.Id
JOIN [Consulta] Co ON E.IdConsulta = Co.Id
JOIN [Paciente] P ON Co.IdPaciente = P.Id
JOIN [Atendimento] AtSolicitado ON Co.IdAtendimento = AtSolicitado.Id
JOIN [Medico] MedQueSolicitou ON AtSolicitado.IdMedico = MedQueSolicitou.Id
GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Atualização concluída.';


GO
