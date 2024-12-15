import logo from "./logo.svg";
import "./App.css";
import { Container, Nav, Navbar } from "react-bootstrap";
import { BrowserRouter, Link, Routes, Route } from "react-router-dom";
import Landing from "./pages/landing";
import Reviews from "./pages/reviews";

function App() {
  return (
    <Container>
      <BrowserRouter>
        <Navbar bg="dark" variant="dark">
          <Navbar.Brand as={Link} to="/">
            AFL Player Ratings
          </Navbar.Brand>
          <Nav className="mr-auto">
            <Nav.Link as={Link} to="/">
              Players
            </Nav.Link>
            <Nav.Link as={Link} to="/reviews">
              Reviews
            </Nav.Link>
          </Nav>
        </Navbar>
        <Routes>
          <Route path="/" element={<Landing />} />
          <Route path="/reviews" element={<Reviews />} />
        </Routes>
      </BrowserRouter>
    </Container>
  );
}

export default App;
