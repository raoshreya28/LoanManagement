using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        // ✅ Get documents for a specific customer
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Document>>> GetByCustomer(int customerId)
        {
            try
            {
                var docs = await _documentService.GetByCustomerAsync(customerId);
                return Ok(docs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching documents for customer {CustomerId}", customerId);
                return Problem($"An error occurred while fetching documents for customer {customerId}.");
            }
        }

        // ✅ Get all pending verification documents
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Document>>> GetPendingVerification()
        {
            try
            {
                var pendingDocs = await _documentService.GetPendingVerificationAsync();
                return Ok(pendingDocs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching pending verification documents.");
                return Problem("An error occurred while fetching pending verification documents.");
            }
        }

        // ✅ Upload (Create)
        [HttpPost]
        public async Task<ActionResult<Document>> Upload([FromBody] Document document)
        {
            if (document == null)
                return BadRequest("Document data is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _documentService.UploadAsync(document);
                return CreatedAtAction(nameof(GetByCustomer),
                    new { customerId = created.CustomerId },
                    created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document.");
                return Problem("An error occurred while uploading the document.");
            }
        }

        // ✅ Update document metadata
        [HttpPut("{id}")]
        public async Task<ActionResult<Document>> Update(int id, [FromBody] Document document)
        {
            if (document == null || document.DocumentId != id)
                return BadRequest("Document ID mismatch.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _documentService.UpdateAsync(document);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating document {DocumentId}", id);
                return Problem($"An error occurred while updating document {id}.");
            }
        }

        // ✅ Mark document as verified
        [HttpPost("{id}/verify")]
        public async Task<IActionResult> Verify(int id)
        {
            try
            {
                await _documentService.VerifyDocumentAsync(id);
                return Ok($"Document {id} verified successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying document {DocumentId}", id);
                return Problem($"An error occurred while verifying document {id}.");
            }
        }
    }
}
