import React, { useEffect, useState } from "react";

function Home() {
  const [employees, setEmployees] = useState([]);
  const [editId, setEditId] = useState(null);

  const [editForm, setEditForm] = useState({
    fullName: "",
    fatherName: "",
    motherName: "",
    dateOfBirth: "",
    gender: "",
    maritalStatus: "",
    spouseName: "",
    email: "",
    mobileNumber: "",
    emergencyContact: "",
    permanentAddress: "",
    currentAddress: "",
    department: "",
    role: "",
    salary: "",
    pfNumber: "",
    taxNumber: ""
  });

  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    try {
      const response = await fetch(
        "https://localhost:7247/api/EmployeeApi/employeehome"
      );

      const data = await response.json();
      setEmployees(data);
    } catch (error) {
      console.error("Fetch Error:", error);
    }
  };

  const handleEdit = (emp) => {
    setEditId(emp.employeeId);

    setEditForm({
      fullName: emp.fullName || "",
      fatherName: emp.fatherName || "",
      motherName: emp.motherName || "",
      dateOfBirth: emp.dateOfBirth
        ? emp.dateOfBirth.substring(0, 10)
        : "",
      gender: emp.gender || "",
      maritalStatus: emp.maritalStatus || "",
      spouseName: emp.spouseName || "",
      email: emp.email || "",
      mobileNumber: emp.mobileNumber || "",
      emergencyContact: emp.emergencyContact || "",
      permanentAddress: emp.permanentAddress || "",
      currentAddress: emp.currentAddress || "",
      department: emp.department || "",
      role: emp.role || "",
      salary: emp.salary || "",
      pfNumber: emp.pfNumber || "",
      taxNumber: emp.taxNumber || ""
    });
  };

  const handleChange = (e) => {
    setEditForm({
      ...editForm,
      [e.target.name]: e.target.value
    });
  };

  const handleUpdate = async () => {
    try {
      const response = await fetch(
        `https://localhost:7247/api/EmployeeApi/employeehome/${editId}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(editForm)
        }
      );

      const message = await response.text();

      alert(message);

      setEditId(null);
      fetchEmployees();
    } catch (error) {
      console.error("Update Error:", error);
      alert("Update Failed");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h2>Employee Home</h2>

      <table border="1" cellPadding="5">
        <thead>
          <tr>
            <th>ID</th>
            <th>Full Name</th>
            <th>Father Name</th>
            <th>Mother Name</th>
            <th>DOB</th>
            <th>Gender</th>
            <th>Marital Status</th>
            <th>Spouse Name</th>
            <th>Email</th>
            <th>Mobile</th>
            <th>Emergency Contact</th>
            <th>Permanent Address</th>
            <th>Current Address</th>
            <th>Department</th>
            <th>Role</th>
            <th>Salary</th>
            <th>PF Number</th>
            <th>Tax Number</th>
            <th>Action</th>
          </tr>
        </thead>

        <tbody>
          {employees.map((emp) => (
            <tr key={emp.employeeId}>
              <td>{emp.employeeId}</td>

              <td>
                {editId === emp.employeeId ? (
                  <input name="fullName" value={editForm.fullName} onChange={handleChange} />
                ) : emp.fullName}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input name="fatherName" value={editForm.fatherName} onChange={handleChange} />
                ) : emp.fatherName}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input name="motherName" value={editForm.motherName} onChange={handleChange} />
                ) : emp.motherName}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    type="date"
                    name="dateOfBirth"
                    value={editForm.dateOfBirth}
                    onChange={handleChange}
                  />
                ) : (
                  emp.dateOfBirth?.substring(0, 10)
                )}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input name="gender" value={editForm.gender} onChange={handleChange} />
                ) : emp.gender}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="maritalStatus"
                    value={editForm.maritalStatus}
                    onChange={handleChange}
                  />
                ) : emp.maritalStatus}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="spouseName"
                    value={editForm.spouseName}
                    onChange={handleChange}
                  />
                ) : emp.spouseName}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input name="email" value={editForm.email} onChange={handleChange} />
                ) : emp.email}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="mobileNumber"
                    value={editForm.mobileNumber}
                    onChange={handleChange}
                  />
                ) : emp.mobileNumber}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="emergencyContact"
                    value={editForm.emergencyContact}
                    onChange={handleChange}
                  />
                ) : emp.emergencyContact}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="permanentAddress"
                    value={editForm.permanentAddress}
                    onChange={handleChange}
                  />
                ) : emp.permanentAddress}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="currentAddress"
                    value={editForm.currentAddress}
                    onChange={handleChange}
                  />
                ) : emp.currentAddress}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="department"
                    value={editForm.department}
                    onChange={handleChange}
                  />
                ) : emp.department}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input name="role" value={editForm.role} onChange={handleChange} />
                ) : emp.role}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="salary"
                    value={editForm.salary}
                    onChange={handleChange}
                  />
                ) : emp.salary}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="pfNumber"
                    value={editForm.pfNumber}
                    onChange={handleChange}
                  />
                ) : emp.pfNumber}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <input
                    name="taxNumber"
                    value={editForm.taxNumber}
                    onChange={handleChange}
                  />
                ) : emp.taxNumber}
              </td>

              <td>
                {editId === emp.employeeId ? (
                  <>
                    <button onClick={handleUpdate}>Save</button>
                    <button onClick={() => setEditId(null)}>Cancel</button>
                  </>
                ) : (
                  <button onClick={() => handleEdit(emp)}>Edit</button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default Home;