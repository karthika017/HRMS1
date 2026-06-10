using Microsoft.AspNetCore.Mvc;
using Npgsql;
using HRMS.Models;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================
        // HEALTH CHECK
        // =========================
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok("Backend Working Successfully");
        }

        // =========================
        // CREATE EMPLOYEE
        // =========================
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee emp)
        {
            try
            {
                using var conn = new NpgsqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));

                conn.Open();

                // =========================
                // INSERT INTO EMPLOYEES
                // =========================
                string query = @"
                INSERT INTO Employees
                (
                    FullName,
                    FatherName,
                    MotherName,
                    DateOfBirth,
                    Gender,
                    MaritalStatus,
                    SpouseName,
                    Email,
                    MobileNumber,
                    EmergencyContact,
                    PermanentAddress,
                    CurrentAddress,
                    Department,
                    Role,
                    Salary,
                    PFNumber,
                    TaxNumber
                )
                VALUES
                (
                    @FullName,
                    @FatherName,
                    @MotherName,
                    @DateOfBirth,
                    @Gender,
                    @MaritalStatus,
                    @SpouseName,
                    @Email,
                    @MobileNumber,
                    @EmergencyContact,
                    @PermanentAddress,
                    @CurrentAddress,
                    @Department,
                    @Role,
                    @Salary,
                    @PFNumber,
                    @TaxNumber
                )
                RETURNING EmployeeId";

                using var cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FullName", emp.FullName ?? "");
                cmd.Parameters.AddWithValue("@FatherName", emp.FatherName ?? "");
                cmd.Parameters.AddWithValue("@MotherName", emp.MotherName ?? "");

                cmd.Parameters.AddWithValue(
                    "@DateOfBirth",
                    emp.DateOfBirth.HasValue
                        ? emp.DateOfBirth.Value
                        : DBNull.Value);

                cmd.Parameters.AddWithValue("@Gender", emp.Gender ?? "");
                cmd.Parameters.AddWithValue("@MaritalStatus", emp.MaritalStatus ?? "");
                cmd.Parameters.AddWithValue("@SpouseName", emp.SpouseName ?? "");
                cmd.Parameters.AddWithValue("@Email", emp.Email ?? "");
                cmd.Parameters.AddWithValue("@MobileNumber", emp.MobileNumber ?? "");
                cmd.Parameters.AddWithValue("@EmergencyContact", emp.EmergencyContact ?? "");
                cmd.Parameters.AddWithValue("@PermanentAddress", emp.PermanentAddress ?? "");
                cmd.Parameters.AddWithValue("@CurrentAddress", emp.CurrentAddress ?? "");
                cmd.Parameters.AddWithValue("@Department", emp.Department ?? "");
                cmd.Parameters.AddWithValue("@Role", emp.Role ?? "");
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@PFNumber", emp.PFNumber ?? "");
                cmd.Parameters.AddWithValue("@TaxNumber", emp.TaxNumber ?? "");

                int employeeId = Convert.ToInt32(cmd.ExecuteScalar());

                // =========================
                // INSERT INTO EMPLOYEE MASTER
                // =========================
                string masterQuery = @"
                INSERT INTO EmployeeMaster
                (
                    EmployeeId,
                    FullName,
                    FatherName,
                    MotherName,
                    DateOfBirth,
                    Gender,
                    MaritalStatus,
                    SpouseName,
                    Email,
                    MobileNumber,
                    EmergencyContact,
                    PermanentAddress,
                    CurrentAddress,
                    Department,
                    Role,
                    Salary,
                    PFNumber,
                    TaxNumber
                )
                VALUES
                (
                    @EmployeeId,
                    @FullName,
                    @FatherName,
                    @MotherName,
                    @DateOfBirth,
                    @Gender,
                    @MaritalStatus,
                    @SpouseName,
                    @Email,
                    @MobileNumber,
                    @EmergencyContact,
                    @PermanentAddress,
                    @CurrentAddress,
                    @Department,
                    @Role,
                    @Salary,
                    @PFNumber,
                    @TaxNumber
                )";

                using var masterCmd = new NpgsqlCommand(masterQuery, conn);

                masterCmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                masterCmd.Parameters.AddWithValue("@FullName", emp.FullName ?? "");
                masterCmd.Parameters.AddWithValue("@FatherName", emp.FatherName ?? "");
                masterCmd.Parameters.AddWithValue("@MotherName", emp.MotherName ?? "");

                masterCmd.Parameters.AddWithValue(
                    "@DateOfBirth",
                    emp.DateOfBirth.HasValue
                        ? emp.DateOfBirth.Value
                        : DBNull.Value);

                masterCmd.Parameters.AddWithValue("@Gender", emp.Gender ?? "");
                masterCmd.Parameters.AddWithValue("@MaritalStatus", emp.MaritalStatus ?? "");
                masterCmd.Parameters.AddWithValue("@SpouseName", emp.SpouseName ?? "");
                masterCmd.Parameters.AddWithValue("@Email", emp.Email ?? "");
                masterCmd.Parameters.AddWithValue("@MobileNumber", emp.MobileNumber ?? "");
                masterCmd.Parameters.AddWithValue("@EmergencyContact", emp.EmergencyContact ?? "");
                masterCmd.Parameters.AddWithValue("@PermanentAddress", emp.PermanentAddress ?? "");
                masterCmd.Parameters.AddWithValue("@CurrentAddress", emp.CurrentAddress ?? "");
                masterCmd.Parameters.AddWithValue("@Department", emp.Department ?? "");
                masterCmd.Parameters.AddWithValue("@Role", emp.Role ?? "");
                masterCmd.Parameters.AddWithValue("@Salary", emp.Salary);
                masterCmd.Parameters.AddWithValue("@PFNumber", emp.PFNumber ?? "");
                masterCmd.Parameters.AddWithValue("@TaxNumber", emp.TaxNumber ?? "");

                masterCmd.ExecuteNonQuery();

                return Ok("Employee Saved Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // =========================
        // GET EMPLOYEES FOR HOME FORM
        // =========================
        [HttpGet("employeehome")]
        public IActionResult GetEmployeeHome()
        {
            try
            {
                var list = new List<Employee>();

                using var conn = new NpgsqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));

                conn.Open();

                string query =
                    "SELECT * FROM EmployeeMaster ORDER BY EmployeeId DESC";

                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? dob = null;

                    if (reader["dateofbirth"] != DBNull.Value)
                    {
                        if (reader["dateofbirth"] is DateOnly dateOnly)
                        {
                            dob = dateOnly.ToDateTime(TimeOnly.MinValue);
                        }
                        else
                        {
                            dob = Convert.ToDateTime(reader["dateofbirth"]);
                        }
                    }

                    list.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["employeeid"]),
                        FullName = reader["fullname"]?.ToString() ?? "",
                        FatherName = reader["fathername"]?.ToString() ?? "",
                        MotherName = reader["mothername"]?.ToString() ?? "",
                        DateOfBirth = dob,
                        Gender = reader["gender"]?.ToString() ?? "",
                        MaritalStatus = reader["maritalstatus"]?.ToString() ?? "",
                        SpouseName = reader["spousename"]?.ToString() ?? "",
                        Email = reader["email"]?.ToString() ?? "",
                        MobileNumber = reader["mobilenumber"]?.ToString() ?? "",
                        EmergencyContact = reader["emergencycontact"]?.ToString() ?? "",
                        PermanentAddress = reader["permanentaddress"]?.ToString() ?? "",
                        CurrentAddress = reader["currentaddress"]?.ToString() ?? "",
                        Department = reader["department"]?.ToString() ?? "",
                        Role = reader["role"]?.ToString() ?? "",
                        Salary = reader["salary"] == DBNull.Value
                            ? 0
                            : Convert.ToDecimal(reader["salary"]),
                        PFNumber = reader["pfnumber"]?.ToString() ?? "",
                        TaxNumber = reader["taxnumber"]?.ToString() ?? ""
                    });
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // =========================
        // UPDATE EMPLOYEE MASTER ONLY
        // =========================
        [HttpPut("employeehome/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee emp)
        {
            try
            {
                using var conn = new NpgsqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));

                conn.Open();

                string query = @"
                UPDATE EmployeeMaster
                SET
                    FullName = @FullName,
                    FatherName = @FatherName,
                    MotherName = @MotherName,
                    DateOfBirth = @DateOfBirth,
                    Gender = @Gender,
                    MaritalStatus = @MaritalStatus,
                    SpouseName = @SpouseName,
                    Email = @Email,
                    MobileNumber = @MobileNumber,
                    EmergencyContact = @EmergencyContact,
                    PermanentAddress = @PermanentAddress,
                    CurrentAddress = @CurrentAddress,
                    Department = @Department,
                    Role = @Role,
                    Salary = @Salary,
                    PFNumber = @PFNumber,
                    TaxNumber = @TaxNumber
                WHERE EmployeeId = @Id";

                using var cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FullName", emp.FullName ?? "");
                cmd.Parameters.AddWithValue("@FatherName", emp.FatherName ?? "");
                cmd.Parameters.AddWithValue("@MotherName", emp.MotherName ?? "");

                cmd.Parameters.AddWithValue(
                    "@DateOfBirth",
                    emp.DateOfBirth.HasValue
                        ? emp.DateOfBirth.Value
                        : DBNull.Value);

                cmd.Parameters.AddWithValue("@Gender", emp.Gender ?? "");
                cmd.Parameters.AddWithValue("@MaritalStatus", emp.MaritalStatus ?? "");
                cmd.Parameters.AddWithValue("@SpouseName", emp.SpouseName ?? "");
                cmd.Parameters.AddWithValue("@Email", emp.Email ?? "");
                cmd.Parameters.AddWithValue("@MobileNumber", emp.MobileNumber ?? "");
                cmd.Parameters.AddWithValue("@EmergencyContact", emp.EmergencyContact ?? "");
                cmd.Parameters.AddWithValue("@PermanentAddress", emp.PermanentAddress ?? "");
                cmd.Parameters.AddWithValue("@CurrentAddress", emp.CurrentAddress ?? "");
                cmd.Parameters.AddWithValue("@Department", emp.Department ?? "");
                cmd.Parameters.AddWithValue("@Role", emp.Role ?? "");
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@PFNumber", emp.PFNumber ?? "");
                cmd.Parameters.AddWithValue("@TaxNumber", emp.TaxNumber ?? "");

                cmd.ExecuteNonQuery();

                return Ok("EmployeeMaster Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}