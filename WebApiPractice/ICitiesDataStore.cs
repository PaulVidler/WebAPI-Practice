﻿using WebApiPractice.Models;

namespace CityInfo.API
{
    public interface ICitiesDataStore
    {
        List<CityDto> Cities { get; set; }
    }
}