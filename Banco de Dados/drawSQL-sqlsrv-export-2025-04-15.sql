CREATE TABLE "Users"(
    "id" BIGINT NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "Email" NVARCHAR(255) NOT NULL,
    "Username" NVARCHAR(255) NOT NULL,
    "Password" NVARCHAR(255) NOT NULL,
    "CreatedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL
);
ALTER TABLE
    "Users" ADD CONSTRAINT "users_id_primary" PRIMARY KEY("id");
CREATE TABLE "Profiles"(
    "id" BIGINT NOT NULL,
    "Profile" NVARCHAR(255) NOT NULL,
    "CreatedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL,
    "UpdatedBy" DATETIME NOT NULL
);
ALTER TABLE
    "Profiles" ADD CONSTRAINT "profiles_id_primary" PRIMARY KEY("id");
CREATE TABLE "Tickets"(
    "id" BIGINT NOT NULL,
    "Description" NVARCHAR(255) NOT NULL,
    "Solution" NVARCHAR(255) NOT NULL,
    "Observations" NVARCHAR(255) NULL,
    "UserOwner" BIGINT NOT NULL,
    "UserResponsible" BIGINT NULL,
    "CreatedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL,
    "UpdatedBy" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Tickets" ADD CONSTRAINT "tickets_id_primary" PRIMARY KEY("id");
CREATE TABLE "Criticality"(
    "id" BIGINT NOT NULL,
    "Criticality" NVARCHAR(255) NOT NULL,
    "CreadtedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL,
    "UpdatedBy" DATETIME NOT NULL
);
ALTER TABLE
    "Criticality" ADD CONSTRAINT "criticality_id_primary" PRIMARY KEY("id");
CREATE TABLE "Status"(
    "id" BIGINT NOT NULL,
    "Status" NVARCHAR(255) NOT NULL,
    "CreatedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL,
    "UpdatedBy" DATETIME NOT NULL
);
ALTER TABLE
    "Status" ADD CONSTRAINT "status_id_primary" PRIMARY KEY("id");
CREATE TABLE "Logs"(
    "id" BIGINT NOT NULL,
    "Action" NVARCHAR(255) NOT NULL,
    "Description" NVARCHAR(255) NOT NULL,
    "CreatedAt" DATETIME NOT NULL
);
ALTER TABLE
    "Logs" ADD CONSTRAINT "logs_id_primary" PRIMARY KEY("id");
CREATE INDEX "logs_action_index" ON
    "Logs"("Action");
CREATE TABLE "Functionalities"(
    "id" BIGINT NOT NULL,
    "Description" NVARCHAR(255) NOT NULL,
    "CreatedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL,
    "UpdatedBy" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Functionalities" ADD CONSTRAINT "functionalities_id_primary" PRIMARY KEY("id");
CREATE TABLE "ProfilesFunctionalities"(
    "ProfileId" BIGINT NOT NULL,
    "FunctionalityId" BIGINT NOT NULL
);
ALTER TABLE
    "ProfilesFunctionalities" ADD CONSTRAINT "profilesfunctionalities_profileid_primary" PRIMARY KEY("ProfileId");
ALTER TABLE
    "ProfilesFunctionalities" ADD CONSTRAINT "profilesfunctionalities_functionalityid_primary" PRIMARY KEY("FunctionalityId");
CREATE TABLE "RootCauses"(
    "id" BIGINT NOT NULL,
    "RootCause" NVARCHAR(255) NOT NULL,
    "CreatedAt" DATETIME NOT NULL,
    "UpdatedAt" DATETIME NOT NULL,
    "UpdatedBy" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "RootCauses" ADD CONSTRAINT "rootcauses_id_primary" PRIMARY KEY("id");
ALTER TABLE
    "Tickets" ADD CONSTRAINT "tickets_id_foreign" FOREIGN KEY("id") REFERENCES "Criticality"("id");
ALTER TABLE
    "RootCauses" ADD CONSTRAINT "rootcauses_id_foreign" FOREIGN KEY("id") REFERENCES "Tickets"("id");
ALTER TABLE
    "ProfilesFunctionalities" ADD CONSTRAINT "profilesfunctionalities_profileid_foreign" FOREIGN KEY("ProfileId") REFERENCES "Profiles"("id");
ALTER TABLE
    "Tickets" ADD CONSTRAINT "tickets_id_foreign" FOREIGN KEY("id") REFERENCES "Status"("id");
ALTER TABLE
    "Users" ADD CONSTRAINT "users_id_foreign" FOREIGN KEY("id") REFERENCES "Profiles"("id");
ALTER TABLE
    "Functionalities" ADD CONSTRAINT "functionalities_id_foreign" FOREIGN KEY("id") REFERENCES "ProfilesFunctionalities"("FunctionalityId");