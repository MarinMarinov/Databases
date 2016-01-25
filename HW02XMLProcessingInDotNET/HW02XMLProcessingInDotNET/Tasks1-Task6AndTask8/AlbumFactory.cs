using System;
using System.Collections.Generic;

namespace Tasks1_Task6AndTask8
{
    public class AlbumFactory
    {
        public Album CreateAlbum(string name, string artist, int year, string producer, decimal price, int numberOfSongs)
        {
            Album album = new Album(name, artist, year, producer, price);
            
            for (int i = 1; i <= numberOfSongs; i++)
            {
                Song song = new Song
                {
                    Name = string.Format("{0} song {1}", name, i),
                    Duration = new TimeSpan(0, 2 + i, 10 + i).ToString()
                };

                album.Songs.Add(song);
            }

            return album;
        }
    }
}