using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Classes.Common
{
    internal class CalculateFreight
    {

        public static double CalculatePalletVolume(PALLETS pallet, string measurementType)
        {
            double palletVolume = 0;

            if (pallet != null)
            {
                switch (measurementType)
                {
                    case "English":
                        palletVolume = (pallet.Length / 12) * (pallet.Width / 12) * (pallet.Height / 12);
                        break;

                    case "Metric":
                        palletVolume = (pallet.Length / 100) * (pallet.Width / 100) * (pallet.Height / 100);
                        break;

                    default:
                        palletVolume = (pallet.Length / 12) * (pallet.Width / 12) * (pallet.Height / 12);
                        break;
                }                              
            }

            return palletVolume;

        }

        public static double CalculatePalletDensity(PALLETS pallet)
        {
            double palletDensity = 0;

            if (pallet != null)
            {
                double palletVolume = pallet.Length * pallet.Width * pallet.Height;
                palletDensity = pallet.Weight / palletVolume;
            }

            return palletDensity;

        }

        public static double CalculatePackageVolume(PACKAGES package, string measurementType)
        {
            double packageVolume = 0;

            if (package != null)
            {
                switch (measurementType)
                {
                    case "English":
                        packageVolume = (package.Length / 12) * (package.Width / 12) * (package.Height / 12);
                        break;

                    case "Metric":
                        packageVolume = (package.Length / 100) * (package.Width / 100) * (package.Height / 100);
                        break;

                    default:
                        packageVolume = (package.Length / 12) * (package.Width / 12) * (package.Height / 12);
                        break;
                }
            }

            return packageVolume;

        }

        public static async Task<FREIGHTCLASSCODES> CalculatedProperFreightClassByDensity(
            PALLETS pallet,
            IFreightClassCodesRepository classCodeRepository)
        {
            FREIGHTCLASSCODES pResult = new();

            if (pallet != null)
            {
                // Calculate the actual density of the pallet
                var density = CalculatePalletDensity(pallet);
                if (density != 0)
                {
                    pResult = await classCodeRepository.MatchCorrectCodeByLinearDensity(density);

                }

            }

            return pResult;

        }
    }
}
