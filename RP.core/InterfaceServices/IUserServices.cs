using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.core.Dtos.UserDto;
using TaskManagement.core.ResponseModel;

namespace TaskManagement.core.InterfaceServices
{
    public interface IUserServices
    {
        public ResponseModel<ReadUserDto> GetUser(int id);
        public ResponseModel<IEnumerable<ReadUserDto>> GetUsers();
        public ResponseModel<ReadUserDto> CreateUser(CreateUserDto user);
        public ResponseModel<ReadUserDto> UpdateUser(int id, UpdateUserDto user);
        public ResponseModel<ReadUserDto> DeleteUser(int id);
        public ResponseModel<ReadUserDto> UpdateUserEmail(int id, string email);
        public ResponseModel<ReadUserDto> GetUserByEmail(string email);

    }
}
