<div id="top"></div>





<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/othneildrew/Best-README-Template">
    <img src="images/logo-semibold.svg" alt="Logo" width="100" height="100">
  </a>

  <h3 align="center">Inventum</h3>

  <p align="center">
    Collabrate your ideas with your team!
    <br />
    <!-- <a href="https://github.com/othneildrew/Best-README-Template"><strong>Explore the docs »</strong></a> -->
    <br />
    <a href="https://inventum-spa-cgrdmz.vercel.app" target="_blank" rel="noreferrer noopener">View Demo (backend not working because heroku :()</a>
    ·
    <a href="https://github.com/CGRDMZ/Inventum/issues">Report Bug</a>
    ·
    <a href="https://github.com/CGRDMZ/Inventum/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

### Boards Screen
[![Board Screen Screen Shot][product-boards]](https://example.com)

### Board Detail
[![Board Detail Screen Shot][product-detail]](https://example.com)

There is a lot of projects for keeping track of ideas, daily tasks or development process. Inventum is one of them. It is never intended to be a real competitor for the websites like Trello vs., but it is a fun project where one can apply what they learned and also create and improve a real application.  

Here's why you should use Inventum:
* Fully customizable cards and boards.
* Basic and clean UI/UX.
* Allows collabrating by inviting your friends.

For sure, we know that there are way better applications doing what Inventum offers for years, and probably doing it way better and consistent. But we want this project to be in a trend where it is always improving so that maybe one day it will get there eventually.

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

Here are the list of the tech we used for building Inventum;

* [React.js](https://reactjs.org/)
* [Chakra-ui](https://chakra-ui.com/)
* [Dotnet Core](https://dotnet.microsoft.com/)
* [Postgresql](https://www.postgresql.org/)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

Before starting anything you may want to update your npm version.
* npm
  ```sh
  npm install npm@latest -g
  ```

### Installation (with docker also not using dev servers not recommended for development)
if you have docker installed, you can use the docker-compose file to run the docker containers. I would recommend using this method if you just want to see how the application works.
```sh
docker-compose build
docker-compose up
```

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/CGRDMZ/Inventum.git
   ```
#### frontend
1. Change directory to the project folder
   ```sh
   cd frontend
   ```
2. Install NPM packages
   ```sh
   npm install
   ```
3. Start the development server for react
   ```js
   npm start

#### backend
1. Change directory to the project folder
   ```sh
    cd backend
   ```
2. Run the development server for dotnet core
   ```sh
   dotnet watch run --project ./src/WebApi/WebApi.csproj --development

#### DB
For the database you can either run a postgresql server on your local machine [(install from here)](https://www.postgresql.org/) or use the docker-compose.db-only.yml file to run a postgresql server on your local machine.

Basically, after downloading docker, use the commands below to build an image and run the container.

```sh
docker-compose -f docker-compose.db-only.yml build
docker-compose -f docker-compose.db-only.yml up
```

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- ROADMAP -->
## Roadmap

(to be updated...)
<!-- - [x] Add Changelog
- [x] Add back to top links
- [ ] Add Additional Templates w/ Examples
- [ ] Add "components" document to easily copy & paste sections of the readme
- [ ] Multi-language Support
    - [ ] Chinese
    - [ ] Spanish -->

See the [open issues](https://github.com/CGRDMZ/Inventum/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

(to be updated...)

* [Choose an Open Source License](https://choosealicense.com)
* [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
* [Malven's Flexbox Cheatsheet](https://flexbox.malven.co/)
* [Malven's Grid Cheatsheet](https://grid.malven.co/)
* [Img Shields](https://shields.io)
* [GitHub Pages](https://pages.github.com)
* [Font Awesome](https://fontawesome.com)
* [React Icons](https://react-icons.github.io/react-icons/search)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-url]: https://github.com/CGRDMZ/Inventum/graphs/contributors
[forks-url]: https://github.com/CGRDMZ/Inventum/network/members
[stars-url]: https://github.com/CGRDMZ/Inventum/stargazers
[issues-url]: https://github.com/CGRDMZ/Inventum/issues
[license-url]: https://github.com/CGRDMZ/Inventum/blob/master/LICENSE.txt
[product-boards]: images/screenshot-boards.png
[product-detail]: images/screenshot-detail.png
