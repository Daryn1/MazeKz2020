using System.ComponentModel;

namespace Housing.Core.Enums
{
    public enum HouseType
    {
        Ничего,
        Вилла,
        Особняк,
        Коттедж,
        Небоскреб,
        Пентхаус,
        Терраса,
        [Description("Жилой дом")]
        Жилой_дом
    }
}
