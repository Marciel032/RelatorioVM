using RelatorioVM.Dominio.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    /// <summary>
    /// Acesse o link abaixo para visualizar a lista completa de cores
    /// <br/>
    /// <see href = "https://www.w3schools.com/tags/ref_colornames.asp" > Lista de cores</see>
    /// </summary>

    public enum TipoCor
    {        
        Indefinido,

        [TipoCor(CorHtml = "#F0F8FF",CorContraste = Black)]
        AliceBlue,

        [TipoCor(CorHtml = "#FAEBD7", CorContraste = Black)]
        AntiqueWhite,

        [TipoCor(CorHtml = "#00FFFF", CorContraste = Black)]
        Aqua,

        [TipoCor(CorHtml = "#7FFFD4", CorContraste = Black)]
        Aquamarine,

        [TipoCor(CorHtml = "#F0FFFF", CorContraste = Black)]
        Azure,

        [TipoCor(CorHtml = "#F5F5DC", CorContraste = Black)]
        Beige,

        [TipoCor(CorHtml = "#FFE4C4", CorContraste = Black)]
        Bisque,

        [TipoCor(CorHtml = "#000000", CorContraste = Azure)]
        Black,

        [TipoCor(CorHtml = "#FFEBCD", CorContraste = Black)]
        BlanchedAlmond,

        [TipoCor(CorHtml = "#0000FF", CorContraste = Azure)]
        Blue,

        [TipoCor(CorHtml = "#8A2BE2", CorContraste = Black)]
        BlueViolet,

        [TipoCor(CorHtml = "#A52A2A", CorContraste = Black)]
        Brown,

        [TipoCor(CorHtml = "#DEB887", CorContraste = Black)]
        BurlyWood,

        [TipoCor(CorHtml = "#5F9EA0", CorContraste = Black)]
        CadetBlue,

        [TipoCor(CorHtml = "#7FFF00", CorContraste = Black)]
        Chartreuse,

        [TipoCor(CorHtml = "#D2691E", CorContraste = Black)]
        Chocolate,

        [TipoCor(CorHtml = "#FF7F50", CorContraste = Black)]
        Coral,

        [TipoCor(CorHtml = "#6495ED", CorContraste = Black)]
        CornflowerBlue,

        [TipoCor(CorHtml = "#FFF8DC", CorContraste = Black)]
        Cornsilk,

        [TipoCor(CorHtml = "#DC143C", CorContraste = Black)]
        Crimson,

        [TipoCor(CorHtml = "#00FFFF", CorContraste = Black)]
        Cyan,

        [TipoCor(CorHtml = "#00008B", CorContraste = Azure)]
        DarkBlue,

        [TipoCor(CorHtml = "#008B8B", CorContraste = Black)]
        DarkCyan,

        [TipoCor(CorHtml = "#B8860B", CorContraste = Black)]
        DarkGoldenRod,

        [TipoCor(CorHtml = "#A9A9A9", CorContraste = Black)]
        DarkGray,

        [TipoCor(CorHtml = "#006400", CorContraste = Azure)]
        DarkGreen,

        [TipoCor(CorHtml = "#BDB76B", CorContraste = Black)]
        DarkKhaki,

        [TipoCor(CorHtml = "#8B008B", CorContraste = Azure)]
        DarkMagenta,

        [TipoCor(CorHtml = "#556B2F", CorContraste = Azure)]
        DarkOliveGreen,

        [TipoCor(CorHtml = "#FF8C00", CorContraste = Black)]
        DarkOrange,

        [TipoCor(CorHtml = "#9932CC", CorContraste = Black)]
        DarkOrchid,

        [TipoCor(CorHtml = "#8B0000", CorContraste = Azure)]
        DarkRed,

        [TipoCor(CorHtml = "#E9967A", CorContraste = Black)]
        DarkSalmon,

        [TipoCor(CorHtml = "#8FBC8F", CorContraste = Black)]
        DarkSeaGreen,

        [TipoCor(CorHtml = "#483D8B", CorContraste = Azure)]
        DarkSlateBlue,

        [TipoCor(CorHtml = "#2F4F4F", CorContraste = Azure)]
        DarkSlateGray,

        [TipoCor(CorHtml = "#00CED1", CorContraste = Black)]
        DarkTurquoise,

        [TipoCor(CorHtml = "#9400D3", CorContraste = Azure)]
        DarkViolet,

        [TipoCor(CorHtml = "#FF1493", CorContraste = Black)]
        DeepPink,

        [TipoCor(CorHtml = "#00BFFF", CorContraste = Black)]
        DeepSkyBlue,

        [TipoCor(CorHtml = "#696969", CorContraste = Azure)]
        DimGray,

        [TipoCor(CorHtml = "#1E90FF", CorContraste = Black)]
        DodgerBlue,

        [TipoCor(CorHtml = "#B22222", CorContraste = Azure)]
        FireBrick,

        [TipoCor(CorHtml = "#FFFAF0", CorContraste = Black)]
        FloralWhite,

        [TipoCor(CorHtml = "#228B22", CorContraste = Azure)]
        ForestGreen,

        [TipoCor(CorHtml = "#FF00FF", CorContraste = Black)]
        Fuchsia,

        [TipoCor(CorHtml = "#DCDCDC", CorContraste = Black)]
        Gainsboro,

        [TipoCor(CorHtml = "#F8F8FF", CorContraste = Black)]
        GhostWhite,

        [TipoCor(CorHtml = "#FFD700", CorContraste = Black)]
        Gold,

        [TipoCor(CorHtml = "#DAA520", CorContraste = Black)]
        GoldenRod,

        [TipoCor(CorHtml = "#808080", CorContraste = Azure)]
        Gray,

        [TipoCor(CorHtml = "#008000", CorContraste = Azure)]
        Green,

        [TipoCor(CorHtml = "#ADFF2F", CorContraste = Black)]
        GreenYellow,

        [TipoCor(CorHtml = "#F0FFF0", CorContraste = Black)]
        HoneyDew,

        [TipoCor(CorHtml = "#FF69B4", CorContraste = Black)]
        HotPink,

        [TipoCor(CorHtml = "#CD5C5C", CorContraste = Black)]
        IndianRed,

        [TipoCor(CorHtml = "#4B0082", CorContraste = Azure)]
        Indigo,

        [TipoCor(CorHtml = "#FFFFF0", CorContraste = Black)]
        Ivory,

        [TipoCor(CorHtml = "#F0E68C", CorContraste = Black)]
        Khaki,

        [TipoCor(CorHtml = "#E6E6FA", CorContraste = Black)]
        Lavender,

        [TipoCor(CorHtml = "#FFF0F5", CorContraste = Black)]
        LavenderBlush,

        [TipoCor(CorHtml = "#7CFC00", CorContraste = Black)]
        LawnGreen,

        [TipoCor(CorHtml = "#FFFACD", CorContraste = Black)]
        LemonChiffon,

        [TipoCor(CorHtml = "#ADD8E6", CorContraste = Black)]
        LightBlue,

        [TipoCor(CorHtml = "#F08080", CorContraste = Black)]
        LightCoral,

        [TipoCor(CorHtml = "#E0FFFF", CorContraste = Black)]
        LightCyan,

        [TipoCor(CorHtml = "#FAFAD2", CorContraste = Black)]
        LightGoldenRodYellow,

        [TipoCor(CorHtml = "#D3D3D3", CorContraste = Black)]
        LightGray,

        [TipoCor(CorHtml = "#90EE90", CorContraste = Black)]
        LightGreen,

        [TipoCor(CorHtml = "#FFB6C1", CorContraste = Black)]
        LightPink,

        [TipoCor(CorHtml = "#FFA07A", CorContraste = Black)]
        LightSalmon,

        [TipoCor(CorHtml = "#20B2AA", CorContraste = Black)]
        LightSeaGreen,

        [TipoCor(CorHtml = "#87CEFA", CorContraste = Black)]
        LightSkyBlue,

        [TipoCor(CorHtml = "#778899", CorContraste = Black)]
        LightSlateGray,

        [TipoCor(CorHtml = "#B0C4DE", CorContraste = Black)]
        LightSteelBlue,

        [TipoCor(CorHtml = "#FFFFE0", CorContraste = Black)]
        LightYellow,

        [TipoCor(CorHtml = "#00FF00", CorContraste = Black)]
        Lime,

        [TipoCor(CorHtml = "#32CD32", CorContraste = Black)]
        LimeGreen,

        [TipoCor(CorHtml = "#FAF0E6", CorContraste = Black)]
        Linen,

        [TipoCor(CorHtml = "#FF00FF", CorContraste = Black)]
        Magenta,

        [TipoCor(CorHtml = "#800000", CorContraste = Azure)]
        Maroon,

        [TipoCor(CorHtml = "#66CDAA", CorContraste = Black)]
        MediumAquaMarine,

        [TipoCor(CorHtml = "#0000CD", CorContraste = Azure)]
        MediumBlue,

        [TipoCor(CorHtml = "#BA55D3", CorContraste = Black)]
        MediumOrchid,

        [TipoCor(CorHtml = "#9370DB", CorContraste = Black)]
        MediumPurple,

        [TipoCor(CorHtml = "#3CB371", CorContraste = Black)]
        MediumSeaGreen,

        [TipoCor(CorHtml = "#7B68EE", CorContraste = Black)]
        MediumSlateBlue,

        [TipoCor(CorHtml = "#00FA9A", CorContraste = Black)]
        MediumSpringGreen,

        [TipoCor(CorHtml = "#48D1CC", CorContraste = Black)]
        MediumTurquoise,

        [TipoCor(CorHtml = "#C71585", CorContraste = Azure)]
        MediumVioletRed,

        [TipoCor(CorHtml = "#191970", CorContraste = Azure)]
        MidnightBlue,

        [TipoCor(CorHtml = "#F5FFFA", CorContraste = Black)]
        MintCream,

        [TipoCor(CorHtml = "#FFE4E1", CorContraste = Black)]
        MistyRose,

        [TipoCor(CorHtml = "#FFE4B5", CorContraste = Black)]
        Moccasin,

        [TipoCor(CorHtml = "#FFDEAD", CorContraste = Black)]
        NavajoWhite,

        [TipoCor(CorHtml = "#000080", CorContraste = Azure)]
        Navy,

        [TipoCor(CorHtml = "#FDF5E6", CorContraste = Black)]
        OldLace,

        [TipoCor(CorHtml = "#808000", CorContraste = Black)]
        Olive,

        [TipoCor(CorHtml = "#6B8E23", CorContraste = Black)]
        OliveDrab,

        [TipoCor(CorHtml = "#FFA500", CorContraste = Black)]
        Orange,

        [TipoCor(CorHtml = "#FF4500", CorContraste = Black)]
        OrangeRed,

        [TipoCor(CorHtml = "#DA70D6", CorContraste = Black)]
        Orchid,

        [TipoCor(CorHtml = "#EEE8AA", CorContraste = Black)]
        PaleGoldenRod,

        [TipoCor(CorHtml = "#98FB98", CorContraste = Black)]
        PaleGreen,

        [TipoCor(CorHtml = "#AFEEEE", CorContraste = Black)]
        PaleTurquoise,

        [TipoCor(CorHtml = "#DB7093", CorContraste = Black)]
        PaleVioletRed,

        [TipoCor(CorHtml = "#FFEFD5", CorContraste = Black)]
        PapayaWhip,

        [TipoCor(CorHtml = "#FFDAB9", CorContraste = Black)]
        PeachPuff,

        [TipoCor(CorHtml = "#CD853F", CorContraste = Black)]
        Peru,

        [TipoCor(CorHtml = "#FFC0CB", CorContraste = Black)]
        Pink,

        [TipoCor(CorHtml = "#DDA0DD", CorContraste = Black)]
        Plum,

        [TipoCor(CorHtml = "#B0E0E6", CorContraste = Black)]
        PowderBlue,

        [TipoCor(CorHtml = "#800080", CorContraste = Azure)]
        Purple,

        [TipoCor(CorHtml = "#663399", CorContraste = Azure)]
        RebeccaPurple,

        [TipoCor(CorHtml = "#FF0000", CorContraste = Black)]
        Red,

        [TipoCor(CorHtml = "#BC8F8F", CorContraste = Black)]
        RosyBrown,

        [TipoCor(CorHtml = "#4169E1", CorContraste = Black)]
        RoyalBlue,

        [TipoCor(CorHtml = "#8B4513", CorContraste = Black)]
        SaddleBrown,

        [TipoCor(CorHtml = "#FA8072", CorContraste = Black)]
        Salmon,

        [TipoCor(CorHtml = "#F4A460", CorContraste = Black)]
        SandyBrown,

        [TipoCor(CorHtml = "#2E8B57", CorContraste = Azure)]
        SeaGreen,

        [TipoCor(CorHtml = "#FFF5EE", CorContraste = Black)]
        SeaShell,

        [TipoCor(CorHtml = "#A0522D", CorContraste = Azure)]
        Sienna,

        [TipoCor(CorHtml = "#C0C0C0", CorContraste = Black)]
        Silver,

        [TipoCor(CorHtml = "#87CEEB", CorContraste = Black)]
        SkyBlue,

        [TipoCor(CorHtml = "#6A5ACD", CorContraste = Azure)]
        SlateBlue,

        [TipoCor(CorHtml = "#708090", CorContraste = Black)]
        SlateGray,

        [TipoCor(CorHtml = "#FFFAFA", CorContraste = Black)]
        Snow,

        [TipoCor(CorHtml = "#00FF7F", CorContraste = Black)]
        SpringGreen,

        [TipoCor(CorHtml = "#4682B4", CorContraste = Black)]
        SteelBlue,

        [TipoCor(CorHtml = "#D2B48C", CorContraste = Black)]
        Tan,

        [TipoCor(CorHtml = "#008080", CorContraste = Azure)]
        Teal,

        [TipoCor(CorHtml = "#D8BFD8", CorContraste = Black)]
        Thistle,

        [TipoCor(CorHtml = "#FF6347", CorContraste = Black)]
        Tomato,

        [TipoCor(CorHtml = "#40E0D0", CorContraste = Black)]
        Turquoise,

        [TipoCor(CorHtml = "#EE82EE", CorContraste = Black)]
        Violet,

        [TipoCor(CorHtml = "#F5DEB3", CorContraste = Black)]
        Wheat,

        [TipoCor(CorHtml = "#FFFFFF", CorContraste = Black)]
        White,

        [TipoCor(CorHtml = "#F5F5F5", CorContraste = Black)]
        WhiteSmoke,

        [TipoCor(CorHtml = "#FFFF00", CorContraste = Black)]
        Yellow,

        [TipoCor(CorHtml = "#9ACD32", CorContraste = Black)]
        YellowGreen,
    }
}
