using JetBrains.Annotations;
using Pusula.Training.HealthCare.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Countries;

public class Country: AuditedEntity<Guid>
{
    [NotNull]
    public string Name { get; set; }
    [NotNull]
    public string Code { get; set; }
    //public virtual ICollection<City> Cities { get; set; } = new List<City>();

    protected Country()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    public Country(Guid id, string name, string code)
    {
        Id = id;

        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), CountryConsts.NameMaxLength);
        Name = name;

        Check.NotNull(code, nameof(code));
        Check.Length(code, nameof(code), CountryConsts.CodeMaxLength, CountryConsts.CodeMinLength);
        Code = code;
    }
}
