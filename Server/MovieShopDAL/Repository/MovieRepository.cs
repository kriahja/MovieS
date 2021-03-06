﻿using MovieShopDAL.Context;
using MovieShopDAL.DomainModel;
using MovieShopDAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDAL.Repository
{
    public class MovieRepository : IRepository<Movie>
    {
        private List<Movie> movies = new List<Movie>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Movie> ReadAll()
        {
            using (var ctx = new MovieShopContext())
            {
                return ctx.Movies.Include("Genres").ToList();
            }
        }
        public void Add(Movie movie)
        {
            using (var ctx = new MovieShopContext())
            {
                ctx.Movies.Attach(movie);
                ctx.Movies.Add(movie);
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Movie movie = Find(id);
            using (var ctx = new MovieShopContext())
            {


                ctx.Movies.Attach(movie);
                //var thisMovie =  ctx.Movies.Where(x => x.Id == movie.Id).FirstOrDefault();
                ctx.Movies.Remove(movie);
                ctx.SaveChanges();
            }
        }


        public void Edit(Movie movie)
        {
            using (var ctx = new MovieShopContext())
            {
                if (movie == null)
                {
                    throw new ArgumentNullException("movie");
                }
                List<Genre> ge = new List<Genre>();
                GenreRepository genrerep = new GenreRepository();
                Genre g;
                for (int i = 0; i < movie.Genres.Count(); ++i)
                {
                    g = genrerep.Find(movie.Genres.ElementAt(i).Id);
                    movie.Genres.ElementAt(i).Name = g.Name;
                }
                ctx.Entry(movie).State = EntityState.Modified;
                ctx.SaveChanges();


                ////

                //var movieDB = ctx.Movies.FirstOrDefault(x => x.Id == movie.Id);

                //for (int i = 0; i < movie.Genres.Count(); ++i)
                //{
                //    movie.Genres.CopyTo(movieDB.Genres.ToList(),0);
                //    movieDB.Genres.Add(movie.Genres.ElementAt(i));
                //}

                //movieDB.Title = movie.Title;
                //movieDB.Price = movie.Price;
                //movieDB.Year = movie.Year;
                //movieDB.Description = movie.Description;
                //movieDB.url = movie.url;
                //movieDB.MovieCoverUrl = movie.MovieCoverUrl;



                //ctx.SaveChanges();


            }
        }

        public Movie Find(int id)
        {

                foreach (var item in ReadAll())
                {
                    if (item.Id == id)
                    {
                        return item;
                    }

                }
                return null;
            }
        }
}
