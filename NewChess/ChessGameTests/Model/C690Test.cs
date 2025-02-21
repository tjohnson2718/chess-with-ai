using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ChessGame;
using ChessGame.Model;
using ChessGame.Service;
using Moq;
using NUnit.Framework;

namespace ChessGameTests.Model
{
    [TestFixture]
    public class C690Test
    {
        private List<int> _positions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        private Mock<IMovementService> _movementServiceMock;
        private Mock<IEngineAdapterService> _engineAdapterSerciceMock;
        private Controller _controller;

        [SetUp]
        public void SetUp()
        {
            _movementServiceMock = new Mock<IMovementService>();
            _engineAdapterSerciceMock = new Mock<IEngineAdapterService>();
            _controller = new Controller(_movementServiceMock.Object, _engineAdapterSerciceMock.Object);
        }

        [Test]
        public void GetRookPositions_Should_Return_Int_If_RookOne_is_NegativeOne()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

            int actual = _controller.GetRookPositions_690(avail_pos, -1, -1);

            Assert.That(actual, Is.Not.EqualTo(-1));
            Assert.That(actual, Is.InRange(0, 7));
        }

        [Test]
        public void GetRookPositions_Should_Return_Int_If_RookTwo_is_NegativeOne()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

            int actual = _controller.GetRookPositions_690(avail_pos, 0, -1);
            Assert.That(actual, Is.Not.EqualTo(0));
            Assert.That(actual, Is.Not.EqualTo(-1));
            Assert.That(actual, Is.InRange(0, 7));
        }

        [Test]
        public void GetRookPositions_Should_Return_Int_With_Space_For_King_In_Between()
        {
            List<int> avail_pos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7};
            int smallest_expected = 2;

            for (int i = 0; i <= 7; i++)
            {
                if (i == 3 || i == 4 || i == 5) continue;

                int init_actual = _controller.GetRookPositions_690(avail_pos, 4, i);
                int actual = Math.Abs(init_actual - 4);
                Assert.That(actual, Is.GreaterThanOrEqualTo(smallest_expected));
                Assert.That(init_actual, Is.Not.EqualTo(4));
                avail_pos = _positions;
            }
        }

        [Test]
        public void GetKingPosition_Should_Return_Positive_Int_In_Range_Of_Rooks()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

            int rookOne_Position = 1;
            int rookTwo_Position = 5;
            int actual = _controller.GetKingPosition_690(avail_pos, rookOne_Position, rookTwo_Position);

            Assert.That(actual, Is.Not.EqualTo(-1));
            Assert.That(actual, Is.Not.EqualTo(rookOne_Position));
            Assert.That(actual, Is.Not.EqualTo(rookTwo_Position));
            Assert.That(actual, Is.InRange(rookOne_Position, rookTwo_Position));
        }

        [Test]
        public void GetKingPosition_Should_Return_One_Above_Lowest_Rook_If_Dif_Is_Two()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            int expected = 6;
            int rookOne_Position = 5;
            int rookTwo_Position = 7;
            int actual = _controller.GetKingPosition_690(avail_pos, rookOne_Position, rookTwo_Position);

            Assert.That(actual, Is.Not.EqualTo(-1));
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetBishopPositions_Should_Return_Int_If_BishopOne_is_NegativeOne()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            int actual = _controller.GetBishopPositions_690(avail_pos, -1, -1);
            Assert.That(actual, Is.Not.EqualTo(-1));
            Assert.That(actual, Is.InRange(0, 7));
        }

        [Test]
        public void GetBishopPositions_Should_Return_Int_If_BishopTwo_is_NegativeOne()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            int actual = _controller.GetBishopPositions_690(avail_pos, 0, -1);
            Assert.That(actual, Is.Not.EqualTo(0));
            Assert.That(actual, Is.Not.EqualTo(-1));
            Assert.That(actual, Is.InRange(0, 7));
        }

        [Test]
        public void GetBishopPositions_Should_Return_Different_Color_Spaces()
        {
            List<int> avail_pos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            int bishopOne_Position = _controller.GetBishopPositions_690(avail_pos, -1, -1);
            int bishopTwo_Position = _controller.GetBishopPositions_690(avail_pos, bishopOne_Position, -1);

            Assert.That(bishopOne_Position % 2, Is.Not.EqualTo(bishopTwo_Position % 2));
        }

    }
}
