using Xunit;
using Moq;
using AutoMapper;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using AIssist.Domain.Services.Interfaces;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;
using AIssist.Application.Services;

public class ProfileAppServiceTests
{
    private readonly Mock<IProfileService> _profileServiceMock;
    private readonly Mock<ILogAppService> _logAppServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ProfileAppService _service;

    public ProfileAppServiceTests()
    {
        _profileServiceMock = new Mock<IProfileService>();
        _logAppServiceMock = new Mock<ILogAppService>();
        _mapperMock = new Mock<IMapper>();

        _service = new ProfileAppService(
            _profileServiceMock.Object,
            _mapperMock.Object,
            _logAppServiceMock.Object
        );
    }

    [Fact]
    public async Task Add_Should_Return_Success_When_All_Ok()
    {
        var request = new ProfileRequest();
        var profile = new Profiles();

        _mapperMock.Setup(m => m.Map<Profiles>(request)).Returns(profile);

        _profileServiceMock
            .Setup(s => s.Add(profile))
            .ReturnsAsync(true);

        _logAppServiceMock
            .Setup(s => s.Add("Inserção", It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var result = await _service.Add(request);

        Assert.True(result.Success);
        Assert.Null(result.Message);
    }

    [Fact]
    public async Task Add_Should_Return_Error_When_ProfileService_Fails()
    {
        var request = new ProfileRequest();
        var profile = new Profiles();

        _mapperMock.Setup(m => m.Map<Profiles>(request)).Returns(profile);

        _profileServiceMock
            .Setup(s => s.Add(profile))
            .ThrowsAsync(new Exception("Erro"));

        var result = await _service.Add(request);

        Assert.False(result.Success);
        Assert.Equal("Falha ao salvar registro.", result.Message);
    }

    [Fact]
    public async Task Add_Should_Return_Error_When_Log_Fails()
    {
        var request = new ProfileRequest();
        var profile = new Profiles();

        _mapperMock.Setup(m => m.Map<Profiles>(request)).Returns(profile);

        _profileServiceMock
            .Setup(s => s.Add(profile))
            .ReturnsAsync(true);

        var tcsLog = new TaskCompletionSource();
        tcsLog.SetException(new Exception("Erro log"));

        _logAppServiceMock
            .Setup(s => s.Add("Inserção", It.IsAny<string>()))
            .Returns(tcsLog.Task);

        var result = await _service.Add(request);

        Assert.False(result.Success);
        Assert.Equal("Falha ao salvar o log da operação.", result.Message);
    }

    [Fact]
    public async Task Update_Should_Return_Success_When_All_Ok()
    {
        var request = new ProfilePutRequest();
        var profile = new Profiles();

        _mapperMock.Setup(m => m.Map<Profiles>(request)).Returns(profile);

        _profileServiceMock
            .Setup(s => s.Update(profile))
            .ReturnsAsync(true);

        _logAppServiceMock
            .Setup(s => s.Add("Atualização", It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var result = await _service.Update(request);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task Update_Should_Return_Error_When_ProfileService_Fails()
    {
        var request = new ProfilePutRequest();
        var profile = new Profiles();

        _mapperMock.Setup(m => m.Map<Profiles>(request)).Returns(profile);

        _profileServiceMock
            .Setup(s => s.Update(profile))
            .ThrowsAsync(new Exception("Erro"));

        var result = await _service.Update(request);

        Assert.False(result.Success);
        Assert.Equal("Falha ao atualizar registro.", result.Message);
    }

    [Fact]
    public async Task Update_Should_Return_Error_When_Log_Fails()
    {
        var request = new ProfilePutRequest();
        var profile = new Profiles();

        _mapperMock.Setup(m => m.Map<Profiles>(request)).Returns(profile);

        _profileServiceMock
            .Setup(s => s.Update(profile))
            .ReturnsAsync(true);

        var tcsLog = new TaskCompletionSource();
        tcsLog.SetException(new Exception("Erro log"));

        _logAppServiceMock
            .Setup(s => s.Add("Atualização", It.IsAny<string>()))
            .Returns(tcsLog.Task);

        var result = await _service.Update(request);

        Assert.False(result.Success);
        Assert.Equal("Falha ao salvar o log da operação.", result.Message);
    }

    [Fact]
    public async Task Inactivate_Should_Return_Success_When_All_Ok()
    {
        long id = 10;

        _profileServiceMock
            .Setup(s => s.Inactivate(id))
            .ReturnsAsync(true);


        _logAppServiceMock
            .Setup(s => s.Add("Inativação", It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var result = await _service.Inactivate(id);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task Inactivate_Should_Return_Error_When_ProfileService_Fails()
    {
        long id = 10;

        _profileServiceMock
            .Setup(s => s.Inactivate(id))
            .ThrowsAsync(new Exception("Erro"));

        var result = await _service.Inactivate(id);

        Assert.False(result.Success);
        Assert.Equal("Falha ao inativar registro.", result.Message);
    }

    [Fact]
    public async Task Inactivate_Should_Return_Error_When_Log_Fails()
    {
        long id = 10;

        _profileServiceMock
            .Setup(s => s.Inactivate(id))
            .ReturnsAsync(true);


        var tcsLog = new TaskCompletionSource();
        tcsLog.SetException(new Exception("Erro log"));

        _logAppServiceMock
            .Setup(s => s.Add("Inativação", It.IsAny<string>()))
            .Returns(tcsLog.Task);

        var result = await _service.Inactivate(id);

        Assert.False(result.Success);
        Assert.Equal("Falha ao salvar o log da operação.", result.Message);
    }

    [Fact]
    public async Task Get_Should_Call_ProfileService()
    {
        var list = new List<Profiles>() { new Profiles() };

        _profileServiceMock.Setup(s => s.Get()).Returns(Task.FromResult(list));

        var result = await _service.Get();

        Assert.Equal(list, result);
    }

    [Fact]
    public async Task GetById_Should_Call_ProfileService()
    {
        var profile = new Profiles();

        _profileServiceMock.Setup(s => s.GetById(1)).Returns(Task.FromResult<Profiles?>(profile));

        var result = await _service.GetById(1);

        Assert.Equal(profile, result);
    }
}
