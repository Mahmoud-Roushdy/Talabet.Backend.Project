﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.SpecificationEvaluator
{
    public static class EspicificationEvaluator<TEntity>  where TEntity : BaseEntity
    { 
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria); 
            if (spec.IsPagintionEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);
             

             query = spec.Includes.Aggregate(query,(current, include) => current.Include(include));
            return query;
        }
    }
}
