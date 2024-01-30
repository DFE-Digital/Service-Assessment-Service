﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceAssessmentService.Application.UseCases;
using ServiceAssessmentService.Domain.Model;

namespace ServiceAssessmentService.WebApp.Pages.Admin.AssessmentTypes;

public class ViewModel : PageModel
{
    private readonly AssessmentTypeRepository _assessmentTypeRepository;
    private readonly ILogger<ViewModel> _logger;

    public ViewModel(AssessmentTypeRepository assessmentTypeRepository, ILogger<ViewModel> logger)
    {
        _assessmentTypeRepository = assessmentTypeRepository;
        _logger = logger;
    }

    [BindProperty]
    public AssessmentType? AssessmentType { get; set; }

    public async Task<IActionResult> OnGet(Guid id)
    {
        AssessmentType = await _assessmentTypeRepository.GetAssessmentTypeByIdAsync(id);
        if (AssessmentType is null)
        {
            return NotFound($"AssessmentType with ID {id} not found");
        }

        return new PageResult();
    }
}
