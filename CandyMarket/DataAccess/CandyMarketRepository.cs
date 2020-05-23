using CandyMarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using CandyMarket.Models.ViewModels;

namespace CandyMarket.DataAccess
{
    public class CandyMarketRepository
    {
        const string ConnectionString = "Server=localhost;Database=CandyMarket;Trusted_Connection=True;";

        public IEnumerable<Candy> GetAllCandy()
        {
            using(var db = new SqlConnection(ConnectionString))
            {
                return db.Query<Candy>("select * from candy");
            }
        }

        public List<CandyByUser> GetCandyByUser(int id)
        {
            var sql = @"select UserCandy.Id, Candy.Name, Candy.Manufacturer as Manufacturer, Candy.FlavorCategory as FlavorCategory, UserCandy.DateReceived as DateReceived
                        from Candy
	                        join UserCandy on Candy.ID = UserCandy.CandyId
                                join[User] on UserCandy.UserId = [User].ID
                                    where[User].ID = @id
                                        and isEaten = 0";

            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { Id = id};
                var candy = db.Query<CandyByUser>(sql, parameters).ToList();
                return candy;
            }
        }

        public UserCandy EatCandy(int userId, int candyId)
        {
            var candies = GetCandyByUser(userId);
            var candyNameToEat = GetCandyById(candyId);

            var oldestCandy = candies.OrderBy(c => c.DateReceived).FirstOrDefault();
            var oldestCandyId = oldestCandy.Id;

            var sql = @"Update UserCandy
                        set isEaten = 1
                        where UserCandy.Id = @oldestCandyId;";


            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { OldestCandyId = oldestCandyId };
                db.Execute(sql, parameters);
                var userCandy = GetUserCandyById(oldestCandy.Id);
                return userCandy;
            }
        }

        public UserCandy GetUserCandyById(int userCandyId)
        {
            var sql = @"select * from UserCandy where userCandy.Id = @userCandyId";
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { UserCandyId = userCandyId };
                var userCandy = db.QueryFirstOrDefault<UserCandy> (sql, parameters);
                return userCandy;
            }            
        }

        public Candy GetCandyById(int candyId)
        {
            var sql = @"select * from Candy where Candy.Id = @candyId";
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { CandyId = candyId };
                var candy = db.QueryFirstOrDefault<Candy>(sql, parameters);
                return candy;
            }
        }
    }
}
