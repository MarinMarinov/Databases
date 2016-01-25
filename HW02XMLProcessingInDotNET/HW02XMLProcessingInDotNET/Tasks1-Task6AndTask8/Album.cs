using System.Collections.Generic;

namespace Tasks1_Task6AndTask8
{
    public class Album
    {
        private List<Song> songs;

        public Album()
        {
            
        }

        public Album(string name, string artist, int year, string producer, decimal price)
        {
            this.Name = name;
            this.Artist = artist;
            this.Year = year;
            this.Producer = producer;
            this.Price = price;
            this.songs = new List<Song>();
        }

        // For each album you should define: `name`, `artist`, `year`, `producer`, `price` and a list of `songs`.

        public string Name { get; set; }

        public string Artist { get; set; }

        public int Year { get; set; }

        public string Producer { get; set; }

        public decimal Price { get; set; }

        public List<Song> Songs
        {
            get
            {
                return this.songs;
            }

            set { this.songs = value; }
        }

        public List<Song> GetSongs()
        {
            return this.Songs;
        }

        public void AddSong(Song song)
        {
            this.songs.Add(song);
        }

    }
}