using Moq;
using AutoMapper;
using AIssist.Application.Services;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Ticket;
using AIssist.Domain.Services.Interfaces;
namespace AIssist.Tests.ApplicationServices
{
    public class TicketAppServiceTests
    {
        private readonly Mock<ITicketService> _ticketServiceMock;
        private readonly Mock<ILogAppService> _logAppServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TicketAppService _service;

        public TicketAppServiceTests()
        {
            _ticketServiceMock = new Mock<ITicketService>();
            _logAppServiceMock = new Mock<ILogAppService>();
            _mapperMock = new Mock<IMapper>();

            _service = new TicketAppService(
                _ticketServiceMock.Object,
                _logAppServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Add_ShouldReturnSuccess_WhenTicketCreatedAndLogSaved()
        {
            var request = new TicketPostRequest();
            var mapped = new Tickets();

            _mapperMock.Setup(m => m.Map<Tickets>(request)).Returns(mapped);

            _ticketServiceMock.Setup(s => s.Add(mapped)).ReturnsAsync(true);

            _logAppServiceMock
                .Setup(l => l.Add("Inserção", It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await _service.Add(request);

            Assert.True(result.Success);
            Assert.Null(result.Message);
        }

        [Fact]
        public async Task Add_ShouldReturnFail_WhenTicketCreationFails()
        {
            var request = new TicketPostRequest();
            var mapped = new Tickets();

            _mapperMock.Setup(m => m.Map<Tickets>(request)).Returns(mapped);

            _ticketServiceMock.Setup(s => s.Add(mapped)).ReturnsAsync(false);

            var result = await _service.Add(request);

            Assert.False(result.Success);
            Assert.Equal("Falha ao salvar registro.", result.Message);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenUpdateAndLogOk()
        {
            var request = new TicketPutRequest();
            var mapped = new Tickets();

            _mapperMock.Setup(m => m.Map<Tickets>(request)).Returns(mapped);

            _ticketServiceMock.Setup(s => s.Update(mapped)).ReturnsAsync(true);

            _logAppServiceMock
                .Setup(l => l.Add("Atualização", It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await _service.Update(request);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldFail_WhenUpdateFails()
        {
            var request = new TicketPutRequest();
            var mapped = new Tickets();

            _mapperMock.Setup(m => m.Map<Tickets>(request)).Returns(mapped);

            _ticketServiceMock.Setup(s => s.Update(mapped)).ReturnsAsync(false);

            var result = await _service.Update(request);

            Assert.False(result.Success);
            Assert.Equal("Falha ao atualizar registro.", result.Message);
        }

        [Fact]
        public async Task UpdateStatus_ShouldReturnSuccess_WhenServiceReturnsTrue()
        {
            string ticket = "123";
            long status = 2;

            _ticketServiceMock
                .Setup(s => s.UpdateStatus(ticket, status))
                .ReturnsAsync(true);

            _logAppServiceMock
                .Setup(l => l.Add("Atualização Status", It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateStatus(ticket, status);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateStatus_ShouldReturnFail_WhenServiceReturnsFalse()
        {
            string ticket = "123";
            long status = 2;

            _ticketServiceMock
                .Setup(s => s.UpdateStatus(ticket, status))
                .ReturnsAsync(false);

            var result = await _service.UpdateStatus(ticket, status);

            Assert.False(result.Success);
            Assert.Equal("Falha ao atualizar registro.", result.Message);
        }

        [Fact]
        public async Task UpdateAssignee_ShouldReturnSuccess_WhenServiceReturnsTrue()
        {
            string ticket = "123";
            long assignee = 10;

            _ticketServiceMock
                .Setup(s => s.UpdateAssignee(ticket, assignee))
                .ReturnsAsync(true);

            _logAppServiceMock
                .Setup(l => l.Add("Atualização Assignee", It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateAssignee(ticket, assignee);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateAssignee_ShouldReturnFail_WhenServiceReturnsFalse()
        {
            string ticket = "123";
            long assignee = 10;

            _ticketServiceMock
                .Setup(s => s.UpdateAssignee(ticket, assignee))
                .ReturnsAsync(false);

            var result = await _service.UpdateAssignee(ticket, assignee);

            Assert.False(result.Success);
            Assert.Equal("Falha ao atualizar registro.", result.Message);
        }

        [Fact]
        public async Task Get_ShouldReturnList_WhenServiceReturnsList()
        {
            var list = new List<Tickets>() { new Tickets() };

            _ticketServiceMock.Setup(s => s.Get()).ReturnsAsync(list);

            var result = await _service.Get();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetByTicketNumber_ShouldReturnEntity_WhenFound()
        {
            string ticketNumber = "A100";

            var ticket = new Tickets() { TicketNumber = ticketNumber };

            _ticketServiceMock
                .Setup(s => s.GetByTicketNumber(ticketNumber))
                .ReturnsAsync(ticket);

            var result = await _service.GetByTicketNumber(ticketNumber);

            Assert.NotNull(result);
            Assert.Equal(ticketNumber, result.TicketNumber);
        }
    }

}

