using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using flat_vs_fk.Models;

namespace flat_vs_fk
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FlatApproachManager
    {
        FlatApproachDbContext _dbContext; 

        [GlobalSetup]
        public void GlobalSetup()
        {
            _dbContext = new FlatApproachDbContext();
        }

        [Benchmark]
        public dynamic GetTracks()
        {
            var result = from t in _dbContext.Track
                            join i in _dbContext.MediaType
                            on t.MediaTypeId equals i.MediaTypeId
                            join g in _dbContext.Genre
                            on t.GenreId equals g.GenreId
                            join a in _dbContext.Album
                            on t.AlbumId equals a.AlbumId
                            join ar in _dbContext.Artist
                            on a.ArtistId equals ar.ArtistId
                            select (new {
                                t.Name, 
                                t.Composer, 
                                t.UnitPrice, 
                                AlbumTitle = a.Title, 
                                Artist = ar.Name,
                                MediaType = i.Name, 
                                Genre = g.Name
                            });
            
            return result;
        }

        [Benchmark]
        public Track AddTrack()
        {
            var track = new Track
            {
                Name = "Holy Ground",
                Composer = "David Adeleke",
                Milliseconds = 1000,
                Bytes = 20,
                UnitPrice = 1.5m, 
                MediaTypeId = 1, 
                AlbumId = 1, 
                GenreId = 1
            };

            _dbContext.Track.Add(track);
            _dbContext.SaveChanges();

            return track;
        }
    }
}