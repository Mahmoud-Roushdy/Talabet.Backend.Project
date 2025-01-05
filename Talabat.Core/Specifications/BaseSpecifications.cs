using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Skip { get ; set ; }
        public int Take { get; set; }
        public bool IsPagintionEnabled { get ; set; }

        public BaseSpecifications()
        {
            ///Getall 
        }
        public BaseSpecifications(Expression<Func<T,bool>>? CreiteriaExperssion)
        {
            Criteria = CreiteriaExperssion;
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderByExperssion)
        {
            if (OrderByExperssion != null)
                OrderBy = OrderByExperssion;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByExperssionDesc)
        {  if (OrderByExperssionDesc != null)
            OrderBy = OrderByExperssionDesc;
        } 
        public void ApplyPagination (int skip, int take)
        {
            IsPagintionEnabled = true;
            Take = take;
            Skip = skip;
        }
    }
}
