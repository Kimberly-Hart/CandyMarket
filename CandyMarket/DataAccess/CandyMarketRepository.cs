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

        public RandomCandy EatRandomCandyByFlavor(int userId, string flavorCategory)
        {
            var sql = @"select DateReceived, CandyId, UserId, Candy.FlavorCategory, UserCandy.Id as UserCandyId
                                from UserCandy
                                join Candy on Candy.Id = UserCandy.CandyId
                                join [User] on [User].Id = UserCandy.UserId
                                where Candy.FlavorCategory = @flavorCategory
                                and UserCandy.isEaten = 0
                                and [User].Id = @userId";

            var sqlUpdate = @"Update UserCandy
                                set isEaten = 1
                                     where UserCandy.Id = @candyToEat;";


            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { UserId = userId, FlavorCategory = flavorCategory };
                var flavorCandy = db.Query<RandomCandy>(sql, parameters).ToList();
                Random rand = new Random();
                var distinctIds = flavorCandy.Select(f => f.CandyId).Distinct();
                var randomCandyIndex = rand.Next(0, flavorCandy.Count());
                var randomCandyId = flavorCandy[randomCandyIndex].CandyId;
                var selectedCandy = flavorCandy.Where(c => c.CandyId == randomCandyId).OrderBy(c => c.DateReceived).FirstOrDefault();
                var candyToEat = selectedCandy.UserCandyId;
                var parameters2 = new { CandyToEat = candyToEat };
                db.Execute(sqlUpdate, parameters2);
                return selectedCandy;
            }
        }

        public string TradeCandy(int userId1, int userId2)
        {
            var user1OwnsCandy = OwnsCandy(userId1);
            var user2OwnsCandy = OwnsCandy(userId2);
            if (user1OwnsCandy && user2OwnsCandy)
            {
                var sql = @"update UserCandy
                        set UserId = case when UserId = @userId1 and isEaten = 0 then @userId2 
                        when UserId = @userId2 and isEaten = 0 then @userId1
                        else UserId end";

                using (var db = new SqlConnection(ConnectionString))
                {
                    var parameters = new { UserId1 = userId1, UserId2 = userId2 };
                    db.Execute(sql, parameters);
                    return $"User {userId1} traded their candy stash with user {userId2}";
                }
            }
            else return "One or more users does not own candy to trade";            
        }

        public bool OwnsCandy(int userId)
        {
            var sql = @"select count(*) as CandyCount
                        from UserCandy
                        where UserId = @userId and isEaten = 0";

            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { UserId = userId };
                var count = db.QueryFirstOrDefault<Count>(sql, parameters);
                if (count.CandyCount == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
