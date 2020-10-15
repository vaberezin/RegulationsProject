using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Regulations.Models;
using Regulations.Models.DatabaseContexts;

namespace Regulations
{
    public static class RegulationDataInit
    {
        public static void Initialize(RegulationContext context)
        {
            if (!context.Regulations.Any())
            {
                context.Regulations.AddRange(
                    new Regulation
                    {
                        ShortName = "SP.30.13330.2016",
                        Added = DateTime.Now,
                        FullName = "trarar",
                        Link = null,
                        UserId = 1,
                        
                        
                    }, new Regulation
                    {
                        ShortName = "SP.31.13330.2012",
                        Added = DateTime.Now,
                        FullName = "trarar",
                        Link = null,
                        UserId = 1,
                        
                    }, new Regulation
                    {
                        ShortName = "SP.32.13330.2018",
                        Added = DateTime.Now,
                        FullName = "trarar",
                        Link = null,
                        UserId = 1,
                        
                    }                
                    );
                context.SaveChanges();
            }
        }
    }
}
