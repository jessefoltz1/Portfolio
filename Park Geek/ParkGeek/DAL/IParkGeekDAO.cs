using ParkGeek.DAL.Models;
using System;
using System.Collections.Generic;

namespace ParkGeek
{
    public interface IParkGeekDAO
    {
        ParkModel GetPark(string parkCode);
        IList<ParkModel> GetParks();
    }
}
