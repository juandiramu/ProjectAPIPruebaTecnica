﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BaseService<TEntity> where TEntity : class, new()
    {
        protected BaseModel<TEntity> _BaseModel;

        public BaseService(BaseModel<TEntity> baseModel)
        {
            _BaseModel = baseModel;
        }

        #region Repository


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _BaseModel.GetAll;
        }

		/// <summary>
		/// Consulta una entidad por Id
		/// </summary>
		public virtual TEntity GetById(object id)
		{
            return _BaseModel.FindById(id);
		}

		/// <summary>
		/// Crea un entidad (Guarda)
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual TEntity Create(TEntity entity)
        {

            return _BaseModel.Create(entity);
        }


        /// <summary>
        /// Actualiza la entidad (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(object id, TEntity editedEntity, out bool changed)
        {
            TEntity originalEntity = _BaseModel.FindById(id);
            return _BaseModel.Update(editedEntity, originalEntity, out changed);
        }


        /// <summary>
        /// Elimina una entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            return _BaseModel.Delete(entity);
        }

		/// <summary>
		/// Obtiene una entidad basada en un filtro específico.
		/// </summary>
		/// <param name="filter">Expresión lambda para filtrar la entidad</param>
		/// <returns>Entidad que cumple con el filtro</returns>
		public virtual IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
		{
			return _BaseModel.GetAll.Where(filter);
		}


		/// <summary>
		/// Guardar cambios
		/// </summary>
		public virtual void SaveChanges()
        {
            _BaseModel.SaveChanges();
        }
        #endregion


    }
}
