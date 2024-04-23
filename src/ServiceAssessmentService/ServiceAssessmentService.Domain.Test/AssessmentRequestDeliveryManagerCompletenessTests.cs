﻿using ServiceAssessmentService.Domain.Model;

namespace ServiceAssessmentService.Domain.Test;

public class AssessmentRequestDeliveryManagerCompletenessTests
{
    private static PersonModel ArbitraryValidPerson => new PersonModel
    {
        PersonalName = "Arbitrary Personal Name",
        FamilyName = "Arbitrary Family Name",
        Email = "arbitrary@example.com",
    };
    private static PersonModel ArbitraryIncompletePerson => new PersonModel
    {
        PersonalName = null,
        FamilyName = null,
        Email = null,
    };

    public static TheoryData<bool?, PersonModel?> CompleteCombinations => new()
    {
        // Complete scenarios
        // - Declares Delivery Manager is known, Delivery Manager details provided
        {true, ArbitraryValidPerson},
        // - Declares Delivery Manager is not known, no Delivery Manager details provided
        {false, null},

    };

    public static TheoryData<bool?, PersonModel?> IncompleteCombinations => new()
    {
        // All other combinations are incomplete/invalid
        {null, null},
        {null, ArbitraryValidPerson},
        {null, ArbitraryIncompletePerson},

        {true, null},
        // {true, ArbitraryValidPerson}, // ignore - valid combination
        {true, ArbitraryIncompletePerson},
        
        // {false, null}, // ignore - valid combination
        {false, ArbitraryValidPerson},
        {false, ArbitraryIncompletePerson},

    };


    [Theory]
    [MemberData(nameof(CompleteCombinations))]
    public void IsDeliveryManagerComplete_WHEN_CompleteCombinationOfValues_THEN_IsComplete(
        bool? hasDeliveryManager,
        PersonModel? deliveryManager
    )
    {
        // Arrange
        var assessmentRequest = new AssessmentRequest
        {
            HasDeliveryManager = hasDeliveryManager,
            DeliveryManager = deliveryManager,
        };

        // Act
        var result = assessmentRequest.IsDeliveryManagerComplete();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(IncompleteCombinations))]
    public void IsDeliveryManagerComplete_WHEN_IncompleteCombinationOfValues_THEN_IsComplete(
        bool? hasDeliveryManager,
        PersonModel? deliveryManager
    )
    {
        // Arrange
        var assessmentRequest = new AssessmentRequest
        {
            HasDeliveryManager = hasDeliveryManager,
            DeliveryManager = deliveryManager,
        };

        // Act
        var result = assessmentRequest.IsDeliveryManagerComplete();

        // Assert
        string deliveryManagerString = deliveryManager?.ToString() ?? "null";

        Assert.False(result, $"Expected to be incomplete: " +
                             $"\n- DeliveryManager: {deliveryManagerString}");
    }
}
