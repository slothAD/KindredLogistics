﻿using Il2CppInterop.Runtime;
using ProjectM;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace KindredLogistics.Services;
class SalvageService
{
    EntityQuery salvageStationQuery;

    public SalvageService()
    {
        var entityQueryBuilder = new EntityQueryBuilder(Allocator.Temp)
            .AddAll(ComponentType.ReadOnly(Il2CppType.Of<Salvagestation>()));
        salvageStationQuery = Core.EntityManager.CreateEntityQuery(ref entityQueryBuilder);
        entityQueryBuilder.Dispose();
    }

    public IEnumerable<Entity> GetAllSalvageStations(int territoryId)
    {
        var stationArray = salvageStationQuery.ToEntityArray(Allocator.Temp);
        try
        {
            foreach (var station in stationArray)
            {
                if (Core.TerritoryService.GetTerritoryId(station) != territoryId) continue;
                yield return station;
            }
        }
        finally
        {
            stationArray.Dispose();
        }
    }
}
