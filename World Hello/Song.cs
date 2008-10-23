using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace World_Hello
{
    [XmlRoot("songList")]
    public class SongList : System.Collections.ObjectModel.Collection<Song>
    {//add doesnt seem to work.
        private ArrayList listOfSongs;

        public SongList()
        {
            listOfSongs = new ArrayList();
        }

        [XmlElement("song")]
        public Song[] Songs
        {
            get
            {
                Song[] songs = new Song[listOfSongs.Count];
                listOfSongs.CopyTo(songs);
                return songs;
            }
            set
            {
                if (value == null) return;
                Song[] songs = (Song[])value;
                listOfSongs.Clear();
                foreach (Song song in songs)
                    listOfSongs.Add(song);
            }
        }

        new public int Add(Song song)
        {
            return listOfSongs.Add(song);
        }
    }
    public class Song
    {
        [XmlAttribute("title")]
        public string title;
        [XmlAttribute("artist")]
        public string artist;
        [XmlAttribute("match")]
        public int match;
        public Song(string title_, string artist_, int match_)
        {
            title = title_;
            artist = artist_;
            match = match_;
        }
        public Song()
        {
        }
        public string Title
        {
            get { return title; }
        }
        public string Artist
        {
            get { return artist; }
        }
        public int Match
        {
            get { return match; }
        }
    }
}
