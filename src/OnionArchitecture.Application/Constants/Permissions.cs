
using System.Collections.Generic;

namespace OnionArchitecture.Application.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }

        public static class Attachments
        {
            public const string View = "Permissions.Attachments.View";
            public const string Create = "Permissions.Attachments.Create";
            public const string Edit = "Permissions.Attachments.Edit";
            public const string Delete = "Permissions.Attachments.Delete";
        }

        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Create = "Permissions.Categories.Create";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Delete = "Permissions.Categories.Delete";
        }

        public static class Commands
        {
            public const string View = "Permissions.Commands.View";
            public const string Create = "Permissions.Commands.Create";
            public const string Edit = "Permissions.Commands.Edit";
            public const string Delete = "Permissions.Commands.Delete";
        }

        public static class CommandFunctions
        {
            public const string View = "Permissions.CommandFunctions.View";
            public const string Create = "Permissions.CommandFunctions.Create";
            public const string Edit = "Permissions.CommandFunctions.Edit";
            public const string Delete = "Permissions.CommandFunctions.Delete";
        }

        public static class Comments
        {
            public const string View = "Permissions.Comments.View";
            public const string Create = "Permissions.Comments.Create";
            public const string Edit = "Permissions.Comments.Edit";
            public const string Delete = "Permissions.Comments.Delete";
        }

        public static class Functions
        {
            public const string View = "Permissions.Functions.View";
            public const string Create = "Permissions.Functions.Create";
            public const string Edit = "Permissions.Functions.Edit";
            public const string Delete = "Permissions.Functions.Delete";
        }

        public static class MyBases
        {
            public const string View = "Permissions.MyBases.View";
            public const string Create = "Permissions.MyBases.Create";
            public const string Edit = "Permissions.MyBases.Edit";
            public const string Delete = "Permissions.MyBases.Delete";
        }

        public static class Labels
        {
            public const string View = "Permissions.Labels.View";
            public const string Create = "Permissions.Labels.Create";
            public const string Edit = "Permissions.Labels.Edit";
            public const string Delete = "Permissions.Labels.Delete";
        }

        public static class LabelMyBases
        {
            public const string View = "Permissions.LabelMyBases.View";
            public const string Create = "Permissions.LabelMyBases.Create";
            public const string Edit = "Permissions.LabelMyBases.Edit";
            public const string Delete = "Permissions.LabelMyBases.Delete";
        }

        public static class Privileges
        {
            public const string View = "Permissions.Privileges.View";
            public const string Create = "Permissions.Privileges.Create";
            public const string Edit = "Permissions.Privileges.Edit";
            public const string Delete = "Permissions.Privileges.Delete";
        }

        public static class Reports
        {
            public const string View = "Permissions.Reports.View";
            public const string Create = "Permissions.Reports.Create";
            public const string Edit = "Permissions.Reports.Edit";
            public const string Delete = "Permissions.Reports.Delete";
        }

        public static class Votes
        {
            public const string View = "Permissions.Votes.View";
            public const string Create = "Permissions.Votes.Create";
            public const string Edit = "Permissions.Votes.Edit";
            public const string Delete = "Permissions.Votes.Delete";
        }
    }
}