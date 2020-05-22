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
            var sql = @"select Candy.Name, Candy.Manufacturer as Manufacturer, Candy.FlavorCategory as FlavorCategory, UserCandy.DateReceived as DateReceived
                        from Candy
	                        join UserCandy on Candy.ID = UserCandy.CandyId
                                join[User] on UserCandy.UserId = [User].ID
                                    where[User].ID = @id";

            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new { Id = id};
                var candy = db.Query<CandyByUser>(sql, parameters).ToList();
                return candy;
            }
        }
    }
}
