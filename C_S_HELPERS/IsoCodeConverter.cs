 public string ConvertTwoLetterNameToThreeLetterName(string name)
 {
     if (name == null)
     {
         throw new ArgumentNullException(nameof(name));
     }

     if (name.Length != 2)
     {
         throw new ArgumentException("name must be two letters.");
     }

     name = name.ToUpper();

     CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
     foreach (CultureInfo culture in cultures)
     {
         try
         {
             RegionInfo region = new RegionInfo(culture.LCID);
             if (region.TwoLetterISORegionName.ToUpper() == name)
             {
                 return region.ThreeLetterISORegionName;
             }
         }
         catch (ArgumentException)
         {
             // Ignore cultures that don't have a corresponding RegionInfo
         }
     }

     return null;
 }

 public string ConvertThreeLetterNameToTwoLetterName(string name)
 {
     if (name.Length != 3)
     {
         throw new ArgumentException("name must be three letters.");
     }

     name = name.ToUpper();

     CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
     foreach (CultureInfo culture in cultures)
     {
         RegionInfo region = new RegionInfo(culture.LCID);
         if (region.ThreeLetterISORegionName.ToUpper() == name)
         {
             return region.TwoLetterISORegionName;
         }
     }

     return null;
 }
