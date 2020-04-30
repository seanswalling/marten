﻿using System;
using Marten.Services;
using Marten.Testing.Documents;
using Shouldly;
using Xunit;

namespace Marten.Testing.Services
{
    public class NulloIdentityMapTests
    {
        [Fact]
        public void get_with_json()
        {
            var serializer = new TestsSerializer();
            var target = new Target();
            var json = serializer.ToJson(target);

            var map = new NulloIdentityMap(serializer);

            var target2 = map.Get<Target>(target.Id, json.ToReader(), null);
            target2.Id.ShouldBe(target.Id);
        }

        [Fact]
        public void get_with_concrete_type()
        {
            var serializer = new JsonNetSerializer();
            var camaro = new Camaro();

            var json = serializer.ToJson(camaro);

            var map = new NulloIdentityMap(serializer);

            map.Get<Car>(camaro.Id, typeof (Camaro), json.ToReader(), null)
                .ShouldBeOfType<Camaro>()
                .Id.ShouldBe(camaro.Id);


        }

        public class Car
        {
            public Guid Id = Guid.NewGuid();
        }

        // I grew up in the Ozarks where Camaro v. Mustang was a big argument
        public class Camaro : Car { }
        public class Mustang : Car { }
    }
}
