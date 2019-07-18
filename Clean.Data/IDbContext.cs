using Clean.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data
{
    public interface IDbContext
    {

        IDbSet<T> Set<T>() where T : BaseEntity;

        //Ми нереалізовуєм цей метод в CleanBaseContext по причині того що він унаслідується  також від DbContext
        //і  по суті містиь вже реалізацію SaveChanges() але з батьківського класу 
        //а питання  чому ми реалізовували метод Set , так для того щоб задати умову що примінити Set можна до обєктів  реалізованих від
        //BaseEntity, а такого метода з такою умовою в DbContext нмає
        int SaveChanges();


        DbEntityEntry<T> Entry<T>(T entity) where T : BaseEntity;
    }
}
