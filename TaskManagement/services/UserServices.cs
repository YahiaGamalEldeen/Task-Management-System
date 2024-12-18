using Mapster;
using TaskManagement.core.Dtos.UserDto;
using TaskManagement.core.Interfaces;
using TaskManagement.core.InterfaceServices;
using TaskManagement.core.ResponseModel;
using TaskManagement.Models;

namespace TaskManagement.services
{
    public class UserServices: IUserServices
    {
        private readonly IUnitOfWork _uniOfWork;
        public UserServices(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }
        public ResponseModel<ReadUserDto> GetUser(int id) 
        {
            var user = _uniOfWork.Users.find(u => u.Id == id, new[] { "task" });
            if (user == null) return ResponseModel<ReadUserDto>.Error("No found id");
            var userDto = user.Adapt<ReadUserDto>();
            return ResponseModel<ReadUserDto>.Success(userDto);
        }
        public ResponseModel<IEnumerable<ReadUserDto>> GetUsers() 
        {
            var users = _uniOfWork.Users.GetAll(new[] { "task" });
            var usersDto = users.Adapt<IEnumerable< ReadUserDto>>();
            return ResponseModel<IEnumerable<ReadUserDto>>.Success(usersDto);
        }
        public ResponseModel<ReadUserDto> CreateUser(CreateUserDto user)
        {
            try {
                var u = new Usery { Name = user.Name, Email = user.Email, Password = user.Password };
                var b = _uniOfWork.Users.add(u);
                _uniOfWork.Complete();
                var readUser = b.Adapt<ReadUserDto>();
                return ResponseModel<ReadUserDto>.Success(readUser);
            }
            catch 
            {
                return ResponseModel<ReadUserDto>.Error("Error");
            }
        }
        public ResponseModel<ReadUserDto> UpdateUser(int id ,UpdateUserDto user)
        {
            var u = _uniOfWork.Users.find(u => u.Id == id);
            if (u == null) return ResponseModel<ReadUserDto>.Error("No found id");

                u.Name = user.Name;
                u.Email = user.Email;
                u.Password = user.Password;
                u.task = u.task;

            try{ 
                _uniOfWork.Users.update(u);
                _uniOfWork.Complete();

                var userDto = u.Adapt<ReadUserDto>();
                return ResponseModel<ReadUserDto>.Success(userDto);
       
               }
            catch 
            {
                return ResponseModel<ReadUserDto>.Error("Error");
            }
}
        public ResponseModel<ReadUserDto> DeleteUser(int id)
        {
            var u = _uniOfWork.Users.find(u => u.Id == id);
            if (u == null) return ResponseModel<ReadUserDto>.Error("No found id");

            try 
            { 
                _uniOfWork.Users.delete(u);
                _uniOfWork.Complete();
                var userDto1 = u.Adapt<ReadUserDto>();
                return ResponseModel<ReadUserDto>.Success(userDto1);
            }
            catch 
            {
                return ResponseModel<ReadUserDto>.Error("Error");
            }
        }
        public ResponseModel<ReadUserDto> UpdateUserEmail(int id, string email)
        {
            var u = _uniOfWork.Users.find(u => u.Id == id);
            if (u == null) return ResponseModel<ReadUserDto>.Error("No found id");
            u.task = u.task;

            try
            {
                _uniOfWork.Users.update(u);
                _uniOfWork.Complete();

                var userDto = u.Adapt<ReadUserDto>();
                return ResponseModel<ReadUserDto>.Success(userDto);

            }
            catch
            {
                return ResponseModel<ReadUserDto>.Error("Error");
            }
        }
        public ResponseModel<ReadUserDto> GetUserByEmail(string email) 
        {
            var user = _uniOfWork.Users.find(u => u.Email == email, new[] { "task" });
            if (user == null) return ResponseModel<ReadUserDto>.Error("No found Email");
            var userDto = user.Adapt<ReadUserDto>();
            return ResponseModel<ReadUserDto>.Success(userDto);
        }
    }
}
