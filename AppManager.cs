using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using flat_vs_fk.Models;
using Microsoft.EntityFrameworkCore;

namespace flat_vs_fk
{
    [MemoryDiagnoser]
    [RankColumn]
    [AllStatisticsColumn]
    public class AppManager
    {
        FkApproachDbContext _dbWithForeignKey;
        FlatApproachDbContext _flatdb;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _dbWithForeignKey = new FkApproachDbContext();
            _flatdb = new FlatApproachDbContext();
        }

        [Benchmark]
        public dynamic GetTracksInDbWithForeignKey()
        {
             var result = _dbWithForeignKey.Track
                        .Include(m => m.MediaType)
                        .Include(m => m.Genre)
                        .Include(m => m.Album)
                        .ThenInclude(m => m.Artist)
                        .Select(m => new {
                            m.Name, 
                            m.Composer, 
                            m.UnitPrice,
                            m.Album.Title,
                            Artist= m.Album.Artist.Name,
                            MediaType = m.MediaType.Name,
                            Genre = m.Genre.Name
                        }).ToList();

            Console.WriteLine($"Fk-Total Tracks: {result.Count()}");
            return result;    
        }
        
        [Benchmark]
        public dynamic GetTracksInFlatDb()
        {
             var result = from t in _flatdb.Track
                            join i in _flatdb.MediaType
                            on t.MediaTypeId equals i.MediaTypeId
                            join g in _flatdb.Genre
                            on t.GenreId equals g.GenreId
                            join a in _flatdb.Album
                            on t.AlbumId equals a.AlbumId
                            join ar in _flatdb.Artist
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
            
            Console.WriteLine($"Flat-Total Tracks: {result.Count()}");
            return result;     
        }

    //     [Benchmark]
    //     public Track AddTrackToDbWithForeignKey()
    //     {
    //         var track = new Track
    //         {
    //             Name = "Holy Ground",
    //             Composer = "David Adeleke",
    //             Milliseconds = 1000,
    //             Bytes = 20,
    //             UnitPrice = 1.5m, 
    //             MediaTypeId = 1, 
    //             AlbumId = 1, 
    //             GenreId = 1
    //         };

    //         _dbWithForeignKey.Track.Add(track);
    //         _dbWithForeignKey.SaveChanges();

    //         return track;
    //     } 

    //     [Benchmark]
    //     public Track AddTrackFlatDb()
    //     {
    //         var track = new Track
    //         {
    //             Name = "Holy Ground",
    //             Composer = "David Adeleke",
    //             Milliseconds = 1000,
    //             Bytes = 20,
    //             UnitPrice = 1.5m, 
    //             MediaTypeId = 1, 
    //             AlbumId = 1, 
    //             GenreId = 1
    //         };

    //         _flatdb.Track.Add(track);
    //         _flatdb.SaveChanges();

    //         return track;
    //     } 
    }
}