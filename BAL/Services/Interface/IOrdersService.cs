using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interface
{
    public interface IOrdersService
    {
        float GetTotalOrderToday();
        float GetTotalOrdersWeek();
        float GetTotalOrderLastThirtyDays();
        int CountOrderLastThirtyDays();

    }
}
