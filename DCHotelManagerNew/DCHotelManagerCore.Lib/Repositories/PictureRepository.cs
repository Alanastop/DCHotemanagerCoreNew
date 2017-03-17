// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PictureRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The picture repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DCHotelManagerCore.Lib.Repositories
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DCHotelManagerCore.Lib.DbContext;
    using DCHotelManagerCore.Lib.Models.Persistent;
    using DCHotelManagerCore.Lib.Repositories.Interfaces;

    #endregion

    /// <summary>
    /// The picture repository.
    /// </summary>
    public class PictureRepository : IEntityRepository<Picture>
    {
        /// <summary>
        /// The data base context.
        /// </summary>
        private readonly DataBaseContext dataBaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureRepository"/> class.
        /// </summary>
        public PictureRepository()
        {
            this.dataBaseContext = new DataBaseContext();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="picture">
        /// The picture.
        /// </param>
        /// <returns>
        /// The <see cref="Picture"/>.
        /// </returns>
        public Picture Create(Picture picture)
        {
            this.dataBaseContext.Pictures.Add(picture);
            try
            {
                this.dataBaseContext.SaveChanges();
            }
            catch (Exception exception)
            {
                throw;
            }

            return picture;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="picturesIds">
        /// The pictures ids.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public void Delete(int[] picturesIds)
        {
            foreach (var id in picturesIds)
            {
                var localPicture = this.dataBaseContext.Pictures.SingleOrDefault(x => x.Id == id);
                if (localPicture == null)
                {
                    throw new ArgumentNullException();
                }

                this.dataBaseContext.Pictures.Remove(localPicture);
                this.dataBaseContext.SaveChanges();
            }
        }

        /// <summary>
        /// The read all list.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Picture> ReadAllList()
        {
            return this.dataBaseContext.Pictures.ToList();
        }

        /// <summary>
        /// The read all query.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<Picture> ReadAllQuery(DataBaseContext context)
        {
            return context.Pictures;
        }

        /// <summary>
        /// The read one.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Picture"/>.
        /// </returns>
        public Picture ReadOne(int id)
        {
            var picture = this.dataBaseContext.Pictures.SingleOrDefault(x => x.Id == id);
            return picture;
        }

        /// <summary>
        /// The set picture.
        /// </summary>
        /// <param name="pictureId">
        /// The picture id.
        /// </param>
        /// <param name="entityName">
        /// The entity name.
        /// </param>
        /// <param name="entityId">
        /// The entity id.
        /// </param>
        public void SetPicture(int pictureId, string entityName, int entityId)
        {
            if (typeof(Hotel).Name == entityName)
            {
                this.SetPictureToHotel(pictureId, entityId);
            }
            else if (typeof(Room).Name == entityName)
            {
                this.SetPictureToRoom(pictureId, entityId);
            }
        }

        /// <summary>
        /// The unset picture.
        /// </summary>
        /// <param name="pictureId">
        /// The picture id.
        /// </param>
        public void UnsetPicture(int pictureId)
        {
            var picture = this.dataBaseContext.Find<Picture>(pictureId);
            if (picture != null)
            {
                picture.RoomId = null;
                picture.HotelId = null;
                this.dataBaseContext.SaveChanges();
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="picture">
        /// The picture.
        /// </param>
        public void Update(Picture picture)
        {
            var databasePicture = this.dataBaseContext.Pictures.SingleOrDefault(x => x.Id == picture.Id);

            if (databasePicture == null)
            {
                return;
            }

            databasePicture.HotelId = picture.HotelId;
            databasePicture.RoomId = picture.RoomId;
            databasePicture.Name = picture.Name;
            this.dataBaseContext.SaveChanges();
        }

        /// <summary>
        /// The set picture to hotel.
        /// </summary>
        /// <param name="pictureId">
        /// The picture id.
        /// </param>
        /// <param name="hotelId">
        /// The hotel id.
        /// </param>
        private void SetPictureToHotel(int pictureId, int hotelId)
        {
            var picture = this.dataBaseContext.Find<Picture>(pictureId);
            if (picture != null)
            {
                picture.HotelId = hotelId;
                this.dataBaseContext.SaveChanges();
            }
        }

        /// <summary>
        /// The set picture to room.
        /// </summary>
        /// <param name="pictureId">
        /// The picture id.
        /// </param>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        private void SetPictureToRoom(int pictureId, int roomId)
        {
            var picture = this.dataBaseContext.Find<Picture>(pictureId);
            if (picture != null)
            {
                picture.RoomId = roomId;
                this.dataBaseContext.SaveChanges();
            }
        }
    }
}