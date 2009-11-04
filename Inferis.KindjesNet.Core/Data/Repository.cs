using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Linq;

namespace Inferis.KindjesNet.Core.Data
{
    public class Repository : IRepository
    {
        private bool disposing = false;

        [Dependency]
        public IUnitOfWorkContext Context { protected get; set; }

        public IQueryable<T> Query<T>()
        {
            return Session.Linq<T>();
        }

        public object Save<T>(T item)
        {
            return Session.Save(item);
        }

        public void SaveOrUpdate<T>(T item)
        {
            Session.SaveOrUpdate(item);
        }

        protected ISession Session
        {
            get
            {
                var session = Context.Items["UnitOfWorkScope_Session"] as ISession;
                if (session == null && !disposing) {
                    var factory = Context.Resolve<ISessionFactory>();
                    session = factory.OpenSession();
                    session.BeginTransaction();
                    Context.Items["UnitOfWorkScope_Session"] = session;
                }

                return session;
            }
        }

        public void Dispose()
        {
            disposing = true;
            if (Session == null) return;

            var session = Session;
            Context.Items["UnitOfWorkScope_Session"] = null;

            var transaction = session.Transaction;
            if (transaction != null) {
                if (IsInException())
                    transaction.Rollback();
                else
                    transaction.Commit();
            }

            session.Close();
            disposing = false;
        }

        private static bool IsInException()
        {
            return Marshal.GetExceptionPointers() != IntPtr.Zero || Marshal.GetExceptionCode() != 0;
        }

    }
}
