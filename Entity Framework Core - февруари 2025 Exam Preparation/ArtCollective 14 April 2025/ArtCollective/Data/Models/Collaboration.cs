using System.ComponentModel.DataAnnotations.Schema;

namespace ArtCollective.Data.Models;

public class Collaboration
{
    //	ArtistOneId – integer, Foreign Key(required)
    //  •	ArtistOne - Artist
    //	ArtistTwoId – integer, Foreign Key(required)
    //  •	ArtistTwo - Artist


    [ForeignKey(nameof(ArtistOne))]
    public int ArtistOneId { get; set; }
    public Artist ArtistOne { get; set; } = null!;





    [ForeignKey(nameof(ArtistTwo))]
    public int ArtistTwoId { get; set; }
    public Artist ArtistTwo { get; set; } = null!;

    //inverse prop da sloja ottatuka
}