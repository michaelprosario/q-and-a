CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Comments" (
    "Id" text NOT NULL,
    "Content" text NULL,
    "HtmlContent" text NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "CreatedBy" text NULL,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedBy" text NULL,
    "ParentEntityType" text NULL,
    "ParentEntityId" text NULL,
    "IsDeleted" boolean NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedBy" text NULL,
    CONSTRAINT "PK_Comments" PRIMARY KEY ("Id")
);

CREATE TABLE "EntityViews" (
    "Id" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "CreatedBy" text NULL,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedBy" text NULL,
    "ParentEntityType" text NULL,
    "ParentEntityId" text NULL,
    "IsDeleted" boolean NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedBy" text NULL,
    CONSTRAINT "PK_EntityViews" PRIMARY KEY ("Id")
);

CREATE TABLE "EntityVotes" (
    "Id" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "CreatedBy" text NULL,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedBy" text NULL,
    "ParentEntityType" text NULL,
    "ParentEntityId" text NULL,
    "IsDeleted" boolean NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedBy" text NULL,
    CONSTRAINT "PK_EntityVotes" PRIMARY KEY ("Id")
);

CREATE TABLE "QuestionAnswers" (
    "Id" text NOT NULL,
    "Votes" integer NOT NULL,
    "QuestionId" text NULL,
    "Name" text NULL,
    "Content" text NULL,
    "HtmlContent" text NULL,
    "Tags" text NULL,
    "PermaLink" text NULL,
    "Status" text NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "CreatedBy" text NULL,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedBy" text NULL,
    "IsDeleted" boolean NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedBy" text NULL,
    "Abstract" text NULL,
    "FeaturedImage" text NULL,
    CONSTRAINT "PK_QuestionAnswers" PRIMARY KEY ("Id")
);

CREATE TABLE "Questions" (
    "Id" text NOT NULL,
    "Votes" integer NOT NULL,
    "Views" integer NOT NULL,
    "AnswerCount" integer NOT NULL,
    "Name" text NULL,
    "Content" text NULL,
    "HtmlContent" text NULL,
    "Tags" text NULL,
    "PermaLink" text NULL,
    "Status" text NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "CreatedBy" text NULL,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedBy" text NULL,
    "IsDeleted" boolean NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedBy" text NULL,
    "Abstract" text NULL,
    "FeaturedImage" text NULL,
    CONSTRAINT "PK_Questions" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220202022713_setup1', '6.0.1');

COMMIT;

