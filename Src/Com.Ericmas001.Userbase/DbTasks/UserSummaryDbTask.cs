﻿using System;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class UserSummaryDbTask : AbstractDbTask
    {
        public UserSummaryDbTask(UserbaseDbContext context) : base(context)
        {

        }

        public UserSummaryResponse Summary(string askingUser, Guid token, string requestedUser)
        {
            var connection = UserbaseSystem.ValidateToken(askingUser, token, Context);

            if (!connection.Success)
                return InvalidResponse;

            User u = Context.Users.SingleOrDefault(x => x.Name.Trim().ToLower() == requestedUser.Trim().ToLower());

            if (u == null)
                return InvalidResponse;

            return new UserSummaryResponse {DisplayName = u.UserProfile.DisplayName, Success = true, Token = connection.Token};
        }

        private UserSummaryResponse InvalidResponse => new UserSummaryResponse
        {
            Token = null,
            Success = false,
            DisplayName = null
        };
    }
}