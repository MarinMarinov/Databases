using System.Collections.Generic;

namespace Tasks1_Task6AndTask8
{
    public class Catalogue
    {
        private List<Album> albums;

        public Catalogue()
        {
            this.albums = new List<Album>();
        }

        public void AddAlbum(Album album)
        {
            this.albums.Add(album);
        }

        public List<Album> GetCatalogue()
        {
            return this.Albums;
        }

        public List<Album> Albums { get { return this.albums; } }
    }
}