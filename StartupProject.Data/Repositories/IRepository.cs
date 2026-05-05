using System;
using System.Collections.Generic;

namespace StartupProject.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);               // ID'ye göre tek bir kayıt getirir
        IEnumerable<T> GetAll();          // Tüm kayıtları liste olarak getirir
        void Add(T entity);               // Yeni kayıt ekler
        void Update(T entity);            // Var olan kaydı günceller
        void Delete(T entity);            // Kaydı siler
    }
}