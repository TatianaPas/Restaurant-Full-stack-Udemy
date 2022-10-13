using Restaurant.DataAccess.Data;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Repository.IRepository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly RestaurantDbContext _db;
        public FoodTypeRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(FoodType foodType)
        {
            var objFromDb = _db.RestaurantFoodType.FirstOrDefault(u => u.Id == foodType.Id);
            objFromDb.Name = foodType.Name;
            Save();
        }
    }
}
