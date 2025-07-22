using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Extensions;

public static class StringExtensions
{
    public static string RemoveNonAnsiCharacters(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var sb = new StringBuilder(input.Length);
        foreach (char c in input)
        {
            if (c >= 0x20 && c <= 0x7E) // printable ASCII range
                sb.Append(c);
        }
        return sb.ToString().Trim();
    }

    private static readonly string[] _reforges =
    [
        "Ancient", "Bizarre", "Burning", "Clean", "Fabled", "Finely", "Forceful",
        "Godly", "Hasty", "Heroic", "Hot", "Legendary", "Lethal", "Mighty",
        "Mythic", "Necrotic", "Pure", "Rapid", "Reinforced", "Spicy", "Strong", "Superior", "Withered",
        "Titanic", "Wise"
    ];

	public static string CleanseItemName(this string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return input;

		foreach (var reforge in _reforges)
		{
			if (input.StartsWith(reforge + " ", StringComparison.OrdinalIgnoreCase))
			{
				return input[reforge.Length..].TrimStart();
			}
		}
		return input.RemoveNonAnsiCharacters();
	}
}
