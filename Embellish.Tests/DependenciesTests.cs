﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using Embellish.Dependencies;

namespace Embellish.Tests
{
	/// <summary>
	/// Description of DependenciesTests.
	/// </summary>
	[TestFixture]
	public class DependenciesTests
	{
		[Test]
		public void CheckAdditionToDomain()
		{
			// Arrange
			var domain = new DependencyDomain<List<int>>();
			
			// Act
			domain.AddToDomain(new List<int>());
			
			// Assert
			Assert.That(domain.DomainItems().Count, Is.EqualTo(1));
		}
		
		[Test]
		public void CheckAdditionToDomainHandlesDuplicates()
		{
			// Arrange
			var domain = new DependencyDomain<List<int>>();
			
			// Act
			var list = new List<int>();
			domain.AddToDomain(list);
			domain.AddToDomain(list);
			
			// Assert
			Assert.That(domain.DomainItems().Count, Is.EqualTo(1));
		}
		
		[Test]
		public void DirectDependencies()
		{
			// Arrange
			var domain = new DependencyDomain<List<int>>();
			var listA = new List<int>();
			var listB = new List<int>();
			domain.AddToDomain(listA);
			domain.AddToDomain(listB);
			domain.AddDependency(listA, listB); // ListA has a dependency on ListB
			
			// Act
			var aDependsUpon = domain.GetDirectDependenciesForObject(listA);
			
			// Assert
			Assert.That(aDependsUpon.Count, Is.EqualTo(1));
		}
		
		[Test]
		public void NonDirectDependencies()
		{
			var domain = new DependencyDomain<string>();
			var stringA = "A";
			var stringB = "B";
			var stringC = "C";
			domain.AddToDomain(stringA);
			domain.AddToDomain(stringB);
			domain.AddToDomain(stringC);
			domain.AddDependency(stringA, stringB); // stringA has a dependency on stringB
			domain.AddDependency(stringB, stringC); // stringB has a dependency on stringC
			
			// Act
			var deps = domain.GetAllDependenciesForObject(stringA);
			
			// Assert
			Assert.That(deps.Count, Is.EqualTo(2));
			Assert.That(deps, Contains.Item(stringB));
			Assert.That(deps, Contains.Item(stringC));
			
		}
		
		[Test]
		public void GeneralDependencyChecks()
		{
			// Arrange
			var domain = new DependencyDomain<string>();
			var stringA = "A";
			var stringB = "B";
			var stringC = "C";
			domain.AddToDomain(stringA);
			domain.AddToDomain(stringB);
			domain.AddToDomain(stringC);
			domain.AddDependency(stringA, stringB); // stringA has a dependency on stringB
			domain.AddDependency(stringB, stringC); // stringB has a dependency on stringC		
			
			// Act
			
			// Assert
			
			Assert.That(domain.DoesADependOnB(stringA,stringB), Is.True);
			Assert.That(domain.DoesADependOnB(stringA, stringC), Is.True);
			Assert.That(domain.DoesADependOnB(stringB, stringA), Is.False);
			Assert.That(domain.DoesADependOnB(stringC, stringA), Is.False);
			
		}
		
		[Test]
		public void GeneralConsumerChecks()
		{
			// Arrange
			var domain = new DependencyDomain<string>();
			var stringA = "A";
			var stringB = "B";
			var stringC = "C";
			domain.AddToDomain(stringA);
			domain.AddToDomain(stringB);
			domain.AddToDomain(stringC);
			domain.AddDependency(stringA, stringB); // stringA has a dependency on stringB
			domain.AddDependency(stringB, stringC); // stringB has a dependency on stringC		
			
			// Act
			
			// Assert
			
			Assert.That(domain.IsAConsumedByB(stringC, stringA), Is.True);
			
		}

		[Test]
		public void DirectConsumersTest()
		{
			// Arrange
			var domain = new DependencyDomain<string>();
			var stringA = "A";
			var stringB = "B";
			var stringC = "C";
			domain.AddToDomain(stringA);
			domain.AddToDomain(stringB);
			domain.AddToDomain(stringC);
			domain.AddDependency(stringA, stringB); // stringA has a dependency on stringB
			domain.AddDependency(stringB, stringC); // stringB has a dependency on stringC		
			
			// Act
			
			// Assert
			
			Assert.That(domain.DirectConsumersOfTarget(stringC)[0] == stringB, Is.True);
			Assert.That(domain.DirectConsumersOfTarget(stringC).Count, Is.EqualTo(1));
			            
		
		}
		
		[Test]
		public void RootLevelItemsTest()
		{
			// Arrange
			var domain = new DependencyDomain<string>();
			var stringA = "A";
			var stringB = "B";
			var stringC = "C";
			domain.AddToDomain(stringA);
			domain.AddToDomain(stringB);
			domain.AddToDomain(stringC);
			domain.AddDependency(stringA, stringB); // stringA has a dependency on stringB
			domain.AddDependency(stringB, stringC); // stringB has a dependency on stringC		
			
			// Act
			var results = domain.RootLevelObjects();
			
			// Assert
			
			Assert.That(results.Count, Is.EqualTo(1));
			Assert.That(results[0], Is.EqualTo("C"));
		
		}
		
	}
}