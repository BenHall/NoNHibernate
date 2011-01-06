using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;

namespace NoNHibernate
{
	class Program
	{
		static void Main(string[] args) {
			Mapper.Reset();
            Mapper.Initialize(x => x.CreateMap<IDataReader, Release>());

            IEnumerable<Release> releases = ExampleQuery();

			foreach (var release in releases) {
				Console.WriteLine(release.productId);
				Console.WriteLine(release.productName);
			}

			Console.ReadLine();
		}

        private static IEnumerable<Release> ExampleQuery()
        {
            Query<Release> query = new Query<Release>()
									.Where(x => x.productName == "Test Product")
									.SetMaxResults(1);

            var executor = new DatabaseQueryExecutor();
            IEnumerable<Release> releases = executor.Execute(query);

			return releases;
		}
	}
}
