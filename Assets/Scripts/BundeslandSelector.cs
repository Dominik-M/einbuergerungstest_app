using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundeslandSelector : MonoBehaviour
{
    public enum BUNDESLAND
    {
        BADEN_WUERTTEMBERG,
        BAYERN,
        BERLIN,
        BRANDENBURG,
        BREMEN,
        HAMBURG,
        HESSEN,
        MECKLENBURG_VORPOMMERN,
        NIEDERSACHSEN,
        NORDRHEIN_WESTFALEN,
        RHEINLAND_PFALZ,
        SAARLAND,
        SACHSEN,
        SACHSEN_ANHALT,
        SCHLESWIG_HOLSTEIN,
        THUERINGEN
    }

    private static BUNDESLAND selectedBundesland;
    public static BUNDESLAND SelectedBundesland { get => selectedBundesland; }

    // Start is called before the first frame update
    void Start()
    {
        Dropdown dropdown = GetComponent<Dropdown>();
        if (dropdown)
        {
            List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
            foreach (BUNDESLAND b in Enum.GetValues(typeof(BUNDESLAND)))
            {
                Dropdown.OptionData option = new Dropdown.OptionData();
                option.text = FormatEnumName(b);
                option.image = LoadSprite("Images/" + b.ToString().ToLowerInvariant());
                data.Add(option);
            }
            dropdown.options = data;
            dropdown.value = 6;
        }
        else
        {
            Debug.Log("Bundesland Dropdown not found!");
        }
    }
    private static readonly Dictionary<BUNDESLAND, string> BundeslandNames = new Dictionary<BUNDESLAND, string>
{
    { BUNDESLAND.BADEN_WUERTTEMBERG, "Baden-Württemberg" },
    { BUNDESLAND.BAYERN, "Bayern" },
    { BUNDESLAND.BERLIN, "Berlin" },
    { BUNDESLAND.BRANDENBURG, "Brandenburg" },
    { BUNDESLAND.BREMEN, "Bremen" },
    { BUNDESLAND.HAMBURG, "Hamburg" },
    { BUNDESLAND.HESSEN, "Hessen" },
    { BUNDESLAND.MECKLENBURG_VORPOMMERN, "Mecklenburg-Vorpommern" },
    { BUNDESLAND.NIEDERSACHSEN, "Niedersachsen" },
    { BUNDESLAND.NORDRHEIN_WESTFALEN, "Nordrhein-Westfalen" },
    { BUNDESLAND.RHEINLAND_PFALZ, "Rheinland-Pfalz" },
    { BUNDESLAND.SAARLAND, "Saarland" },
    { BUNDESLAND.SACHSEN, "Sachsen" },
    { BUNDESLAND.SACHSEN_ANHALT, "Sachsen-Anhalt" },
    { BUNDESLAND.SCHLESWIG_HOLSTEIN, "Schleswig-Holstein" },
    { BUNDESLAND.THUERINGEN, "Thüringen" }
};

    private string FormatEnumName(BUNDESLAND bundesland)
    {
        return BundeslandNames.TryGetValue(bundesland, out string formattedName) ? formattedName : bundesland.ToString();
    }


    public static Sprite LoadSprite(string spritename)
    {
        Debug.Log("Loading Sprite: " + spritename);
        Sprite img = Resources.Load<Sprite>(spritename);
        if (img)
        {
            return img;
        }
        Debug.LogError("Failed to load Sprite: " + spritename);
        return null;
    }

    public void OnValueChange(int value)
    {
        selectedBundesland = (BUNDESLAND)Enum.GetValues(typeof(BUNDESLAND)).GetValue(value);
        Debug.Log("Selected Bundesland: " + SelectedBundesland);
    }
}
